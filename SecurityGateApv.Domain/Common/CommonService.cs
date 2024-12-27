using Microsoft.AspNetCore.Http;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Common
{
    public class CommonService 
    {
        public static async Task<string> ImageToBase64(IFormFile image)
        {
            var ms = new MemoryStream();
            await image.CopyToAsync(ms);          
            return System.Convert.ToBase64String(ms.ToArray());
        }
        public async static Task<string> Encrypt(string clearText)
        {
            string EncryptionKey = "SecurityAPV17";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static async Task<IEnumerable<ValidateVisitDateDTO>> CaculateBusyDates(VisitDetail visit)
        {
            var returnDate = new List<ValidateVisitDateDTO>();
            if (visit.Visit.ScheduleUser == null)
            {
                returnDate.Add(new ValidateVisitDateDTO
                {
                    VisitDate = visit.Visit.ExpectedStartTime,
                    TimeIn = visit.ExpectedStartHour,
                    TimeOut = visit.ExpectedEndHour,
                    VisitId = visit.VisitId,
                });
            }else
            if (visit.Visit.ScheduleUser.Schedule.ScheduleType.ScheduleTypeName == ScheduleTypeEnum.ProcessMonth.ToString())
            {
                var dateOfMonth = visit.Visit.ScheduleUser.Schedule.DaysOfSchedule.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var day in dateOfMonth) {
                    var yearDiff = visit.Visit.ExpectedEndTime.Year - visit.Visit.ExpectedStartTime.Year;
                    var newDate = new DateTime();
                    
                    var monthDiff = 0;
                    if (yearDiff > 0)
                    {
                        monthDiff = (12 - visit.Visit.ExpectedStartTime.Month) + (visit.Visit.ExpectedEndTime.Month - 0) + (yearDiff - 1) * 12 ; 
                    }  
                    else
                    {
                        monthDiff = visit.Visit.ExpectedEndTime.Month - visit.Visit.ExpectedStartTime.Month;
                    }
                    for(int i = 0; i <= monthDiff; i++)
                    {
                        try
                        {
                            var currentYear = visit.Visit.ExpectedStartTime.AddMonths(i).Year;
                            var addDate = new DateTime(currentYear, visit.Visit.ExpectedStartTime.AddMonths(i).Month, int.Parse(day));
                            if(addDate > visit.Visit.ExpectedEndTime || addDate < visit.Visit.ExpectedStartTime)
                            {
                                continue;
                            }
                            returnDate.Add(new ValidateVisitDateDTO
                            {
                                VisitDate = addDate,
                                TimeIn = visit.ExpectedStartHour,
                                TimeOut = visit.ExpectedEndHour,
                                VisitId = visit.VisitId,
                            });
                        }
                        catch
                        {
                            continue;
                        }
   
                    }
                }
            }else if (visit.Visit.ScheduleUser.Schedule.ScheduleType.ScheduleTypeName == ScheduleTypeEnum.ProcessWeek.ToString())
            {
                var dateOfWeek = visit.Visit.ScheduleUser.Schedule.DaysOfSchedule.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var day in dateOfWeek) { 
                    var weekDiff = ((int)(((visit.Visit.ExpectedEndTime - visit.Visit.ExpectedStartTime).TotalDays)/7));
                    var startDate = new DateTime();
                    if (int.Parse(day)-1 >= (int)visit.Visit.ExpectedStartTime.DayOfWeek)
                    {
                        startDate = new DateTime(visit.Visit.ExpectedStartTime.Year, visit.Visit.ExpectedStartTime.Month, visit.Visit.ExpectedStartTime.Day).AddDays(int.Parse(day)-1 - (int)visit.Visit.ExpectedStartTime.DayOfWeek);
                    }
                    else
                    {
                        startDate = new DateTime(visit.Visit.ExpectedStartTime.Year, visit.Visit.ExpectedStartTime.Month, visit.Visit.ExpectedStartTime.Day).AddDays(7 + int.Parse(day)-1 - (int)visit.Visit.ExpectedStartTime.DayOfWeek);
                    }
                    for (int i = 0; i <= weekDiff; i++)
                    {
                        var addDate = startDate.AddDays(i*7);
                        if (addDate > visit.Visit.ExpectedEndTime || addDate < visit.Visit.ExpectedStartTime)
                        {
                            continue;
                        }
                        returnDate.Add(new ValidateVisitDateDTO
                        {
                            VisitDate = addDate,
                            TimeIn = visit.ExpectedStartHour,
                            TimeOut = visit.ExpectedEndHour,
                            VisitId = visit.VisitId,
                        });
                    }
                }
            }
            return returnDate;
        }

        public static async Task<string> Decrypt(string cipherText)
        {
            string EncryptionKey = "SecurityAPV17";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
