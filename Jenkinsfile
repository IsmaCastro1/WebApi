pipeline {
    agent any
    environment {
        SERVER_IP = '10.222.133.60' 
        KEY = 'id_ed25519'
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
                    sh 'dotnet test' // Para ejecutar pruebas
                }
            }
        }
        stage('Build Docker Image') {
            steps {
                echo 'Construyendo la imagen Docker...'
                sh 'docker build -t api .'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Desplegando en la máquina virtual...'
                
                // Exportar la imagen Docker a un archivo tar
                sh 'docker save -o api_image.tar api'

                // Transferir la imagen al servidor
                sh 'scp -i /home/${KEY} api_image.tar user@${SERVER_IP}:/home/user/WebApi'

                // Conectar al servidor y cargar la imagen Docker
                sh 'ssh -i /home/${KEY} user@${SERVER_IP} "docker load -i /home/user/WebApi/api_image.tar"'

                // Detener y eliminar el contenedor existente, si existe
                sh 'ssh -i /home/${KEY} user@${SERVER_IP} "docker stop api || true && docker rm api || true"'

                // Ejecutar el contenedor
                sh 'ssh -i /home/${KEY} user@${SERVER_IP} "docker run -d --name api -p 0:8080 api"'
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
