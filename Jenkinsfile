pipeline {
    agent any
    tools {
        msbuild 'MSBuild 2022'
    }
    stages {
        stage('Build') {
            steps {
                echo 'Construyendo la aplicación...'
                script {
                    bat 'msbuild right-first-time.sln /p:Configuration=Release %MSBUILD_ARGS%'
                }
            }
        }
        // stage('Test') {
        //     steps {
        //         echo 'Ejecutando pruebas...'
        //         // Comando para ejecutar tus pruebas
        //         sh 'mvn test' // Para ejecutar pruebas con Maven
        //     }
        // }
        // stage('Deploy') {
        //     steps {
        //         echo 'Desplegando en la máquina virtual...'
        //         // Comando para desplegar tu aplicación
        //         sh 'scp target/myapp.jar user@your-vm-ip:/path/to/deploy' // Ejemplo de despliegue
        //     }
        // }
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
