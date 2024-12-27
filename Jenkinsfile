pipeline {
    agent any

    stages {
        stage('Git Checkout') {
            steps {
                checkout scmGit(branches: [[name: '*/Vu']], extensions: [], userRemoteConfigs: [[url: 'https://github.com/NhatLe2002/SecurityGateApv_BE.git']])
            }
        }
        stage('Docker Build') {
            agent any
            steps {
                sh 'docker build -t apv_be -f SecurityGateApv.WebApi/Dockerfile .'
            }
        }
        stage('Docker Check Exist Container') {
            agent any
            steps {
                script{
                def inspectExitCode = sh script: "docker inspect apvsecurity", returnStatus: true
                if (inspectExitCode == 0) {
                    sh "docker stop apvsecurity"
                    sh "docker rm apvsecurity"
                    } 
                else{

                    }
                }
            }
        }
        stage('Docker deploy') {
            agent any
            steps {
                sh 'docker run -d --name "apvsecurity" --restart=always -p 8081:8080 apv_be'
            }
        }
        stage('Docker Delete Dangling Image') {
            agent any
            steps {
                sh "docker image prune -f"
            }
        }
    }
    post
    {
            always{
                emailext (
                    subject: "Pipeline Status: ${BUILD_NUMBER}",
                    body: '''<html>
                        <body>
                        <p>Build Status: ${BUILD_STATUS}</p>
                        <p>Build Status: ${BUILD_NUMBER}</p>
                        <p>CheckThe : <a href="${BUILD_URL}">console output</a>.</p>
                        <body>
                     </html>''',
                    to: "luutranvu17@gmail.com",
                    from: "luutranvu123qb@gmail.com",
                    replyTo: "luutranvu123qb@gmail.com",
                    mimeType: "text/html"
                )
            }
    }
}
