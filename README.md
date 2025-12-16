# Proyecto-Final-Progra-3
Repositorio creado para proyecto de Progra III

Guía de instalación
-----
Requisitos del sistema
-----
Visual Studio 2022 actualizado con la carga de trabajo ASP.NET y desarrollo web instalada
.NET SDK versión 8.0
SQL Server o SQL Server Express (LocalDB)
GitHub
-----
Clonación del repositorio

Para obtener el proyecto, en la pantalla de inicio de visual studio escoga Clonar un repositorio, e ingrese el siguiente link

https://github.com/Eduardo-D-Vega/Proyecto-Final-Progra-3.git

Una vez clonado el repositorio, abra el archivo de solución (.sln) utilizando Visual Studio.
-----
Configuración de la base de datos

Abra el archivo `appsettings.json` y verifique la cadena de conexión configurada para Entity Framework Core:

```json
"ConnectionStrings": {
  "ApplicationDbContext": "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=ProyectoFinal;Integrated Security=True"
}
```
La base de datos será creada automáticamente al aplicar las migraciones.
-----

Aplicación de migraciones

Desde Visual Studio, abra la Consola del Administrador de paquetes NuGet en el menú Herramientas.

Ejecute el siguiente comando para crear la base de datos y sus tablas:

```powershell
Update-Database
```

-----

Ejecución del sistema

Una vez aplicadas las migraciones, ejecute el proyecto presionando la tecla F5 o seleccionando la opción Iniciar desde Visual Studio.

La aplicación se abrirá automáticamente en el navegador predeterminado.

-----

Control de acceso

El sistema implementa autenticación y autorización basada en roles.
Los roles disponibles son Administrador, Reclutador y Usuario.
Cada rol tiene acceso restringido a las funcionalidades del sistema según su nivel de permisos.


PDF de la parte escrita:
https://1drv.ms/b/c/0231a0ce43cf5729/IQCuNMp6kY0eRalVyVwGedEHASrpHXe6Mhh1n60S5R35beU?e=kTQGGs
