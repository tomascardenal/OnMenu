# OnMenu
Proyecto final de Desarrollo de Aplicaciones Multiplataforma

## Objetivos / Roadmap
- Interfaz Android, general [X]
- Interfaz Android, ingredientes [X]
- Interfaz Android, recetas [X]
- Interfaz Android, calendario [X]
- Menús en calendario [X]
- Conexión a base local [X]
- Traducciones [X]

## Futuros objetivos / Objetivos descartados en el roadmap inicial
- Interfaz completa iOS (necesidad de un mac en disponibilidad para su creación). [ ]
- Soporte multiidioma de los datos introducidos en la app (distintas recetas e ingredientes para distintos idiomas). [ ]
- Sistema de login de usuarios [ ]
- Generación automática de menús [ ]
- Conexión a API, quedó descartada por problemas con las migraciones de SQLite [ ]

## Tecnologías utilizadas
- Xamarin Android 8.1
- Mono for Android
- SQLite
- sqlite-net-pcl 1.5.231
- C#.NET 7.3
- Conveyor
- Visual Studio 2019
- Linq
- ASP.NET 4.5 + ADO.NET 

## Cambios a lo largo del proyecto / Notas
- Se ha cambiado el uso de Microsoft SQL Server 2017 o bases de Access 16.0 por SQLite por ligereza, coste y tiempo de producción.
- Se ha acortado el roadmap principal por la falta del tiempo inicialmente estimado para la creación de este proyecto. Sin embargo, hay intencionalidad de continuarlo de ser posible, o portarlo a un sistema multiplataforma más moderno.
- Se hicieron testeos simples de una API REST, pero se descartó implementarla finalmente, ya que dio problemas en los últimos días previos a la fecha límite en la conexión a SQLite, sin embargo, se pudo ver la estructura de un servidor REST escrito en .NET y se hicieron pruebas con Conveyor https://conveyor.cloud/ , una extensión que genera una url remota para conectar la aplicación a una API durante el desarrollo.


