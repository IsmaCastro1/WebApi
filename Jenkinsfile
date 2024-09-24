pipeline {
    agent any
    environment {
        SERVER_IP = '10.222.133.60' // Define la variable de entorno para la IP
    }
    tools {
        dotnetsdk 'sdk'
    }
    stages {
        stage('Build') {
            steps {
                echo 'Construyendo la aplicación...'
                script {
                    
                    withEnv(['DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1']) {
                        sh 'dotnet build'
                    }
                   
                }
            }
        }
         stage('Tests') {
             steps {
                 echo 'Ejecutando pruebas...'
                withEnv(['DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1']) {
                 sh 'dotnet test' // Para ejecutar pruebas con Maven
                }
             }
         }
        stage('Deploy') {
             steps {
                 echo 'Desplegando en la máquina virtual...'

                 sh 'scp -i /home/clave.pem -r ./ user@${SERVER_IP}:/home/user/WebApi'

                 sh 'ssh -i /home/clave.pem user@${SERVER_IP} "cd /home/user/WebApi && docker build -t api ."'
                 
                 script {
                    try {
                        sh 'ssh -i /home/clave.pem user@${SERVER_IP} "docker build -t api ."docker stop api || true && docker rm api || true"' 
                    } catch (err) {
                        echo err.getMessage()
                        echo "Error detected, but we will continue."
                    }
                 }
                 
                 sh 'ssh -i /home/clave.pem user@${SERVER_IP} "docker run -d --name api -p 0:8080 api:latest"'
             }
         }
    }
    post {
        success {
            echo 'Pipeline completada con éxito!'
        }
        failure {
            echo 'Pipeline fallida.'
        }
    }
}
