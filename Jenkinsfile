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
                 echo 'Desplegando en la máquina virtual....'

                 sh 'ssh -i /home/clave.pem user@10.222.132.252 "cd /home/user/WebApi && docker build -t api ."'
                 
                 script {
                    try {
                        sh "sudo docker rmi frontend-test"
                    } catch (err) {
                        echo err.getMessage()
                        echo "Error detected, but we will continue."
                    }
                 }
                 
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
