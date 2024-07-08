# Proyecto de Prueba Técnica en Juju

## Descripción

Este proyecto es una prueba técnica desarrollada en Juju. Contiene una API para gestionar entidades como Customers y Post.

## Despliegue de la Aplicación

1. **Requisitos Previos:**
   - Docker instalado en tu máquina. Puedes descargarlo desde [Docker Hub](https://www.docker.com/get-started).

2. **Ejecución de la Aplicación:**
   En la raíz del proyecto, ejecuta el siguiente comando para iniciar los contenedores Docker en segundo plano:

   ```bash
   docker-compose up -d
   ```

   Esto iniciará los contenedores de .NET y SQL Server necesarios para la aplicación.

   - La API se desplegará en `http://localhost:8088`.
   - La base de datos SQL Server estará disponible en el puerto `1436` y la contraseña de la base de datos es `juju123#`.

## Colección de Postman

La colección de Postman para consumir estos endpoints se encuentra en la carpeta `Postman`.

### Instrucciones

1. Abra Postman.
2. Importe la colección desde `Postman/PruebaJuju.postman_collection.json`.

