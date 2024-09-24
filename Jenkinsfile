pipeline {
    agent any
    environment {
        SERVER_IP = '192.168.118.134' // Define la variable de entorno para la IP
        KEY = 'id_rsa'
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
                 echo 'Copiando App'   
                 sh 'scp -i /home/${KEY} -r ./ user@${SERVER_IP}:/home/user/WebApi'

                 echo 'Creando Imagen'  
                 sh 'ssh -i /home/${KEY} user@${SERVER_IP} "cd /home/user/WebApi && docker build -t api ."'
                 
                 script {     
                     
                    try {
                        echo 'Parando contenedor...'
                        sh 'ssh -i /home/${KEY} user@${SERVER_IP} "docker stop api"' 
                         echo 'Borrando contenedor...'
                        sh 'ssh -i /home/${KEY} user@${SERVER_IP} "docker rm api"' 
                    } catch (err) {
                        echo err.getMessage()
                        echo "Error detected, but we will continue."
                    }

                    try {
                        echo 'Borrando imagen...'
                        sh 'ssh -i /home/${KEY} user@${SERVER_IP} "docker rmi api"' 
                    } catch (err) {
                        echo err.getMessage()
                        echo "Error detected, but we will continue."
                    }          
                 }
                 
                 sh 'ssh -i /home/${KEY} user@${SERVER_IP} "docker run -d --name api -p 0:8080 api:latest"'
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
