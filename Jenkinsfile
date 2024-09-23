pipeline {
    agent any
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
                 // Comando para desplegar tu aplicación
                 sh 'ssh -i /home/clave.pem user@10.222.132.252 "cd /home/user/WebApi/Api && docker build -t api ."'
                 
                 sh 'ssh -i /home/clave.pem user@10.222.132.252 "docker build -t api ."docker stop api || true && docker rm api || true"'   

                 sh 'ssh -i /home/clave.pem user@10.222.132.252 "docker run -d --name mi-api -p 80:80 api:latest"'
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
