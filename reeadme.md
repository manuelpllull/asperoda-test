# Asperoda Gas Station Management System

This project is a gas station management system developed in C#.

# Enunciado

## NORMAS A SEGUIR

- El lenguaje utilizado debe ser .NET (VB o C#). Se deja a elección del candidato la elección del lenguaje concreto.
- Realiza una implementación simple. Cíñete a los requerimientos e intenta implementar la solución más simple que te sea posible. Eso sí, no olvides los casos límites.
- Usa una solución de almacenamiento en memoria (por ejemplo, usa colecciones para almacenar la información que consideres necesaria).
- No estamos pidiendo una API REST, Servicio Web o Microservicio. Tan sólo una implementación simple de una librería.
- Focalízate en la calidad del código: Presta atención al diseño orientado a objetos, la limpieza de código y al cumplimiento de principios y estándares de buenas prácticas para diseñar código de calidad (SOLID, etc.).
- Si lo consideras oportuno, adjunta a la solución un fichero "Leeme" donde puedas anotar cualquier detalle o aspecto que nos quieras comentar acerca de tu solución.

## REQUERIMIENTOS DE LA PISTA

Vas a trabajar para una empresa dedicada al mundo de las estaciones de servicio, y nos gustaría que desarrollases una Pista que conozca en todo momento el estado en el que están los surtidores que en ella se encuentran.

La pista debe soportar las siguientes operaciones:

- **Liberar un surtidor**: Cuando se libera un surtidor, este se queda en un estado "libre" por lo que al descolgar su manguera, se puede realizar un suministro sin límite alguno.
- **Prefijar un surtidor**: Es un paso opcional previo a la liberación del surtidor. Con él, se indica en euros un importe máximo a suministrar tras la siguiente liberación del mismo.
- **Bloquear un surtidor**: Es el caso opuesto a la liberación de un surtidor. Al bloquear un surtidor, no es posible realizar ningún suministro a través de él. Este debe ser el estado inicial de todos los surtidores de la pista. Debe tenerse presente que esta acción debe "borrar" del surtidor el posible importe prefijado por una orden de prefijado previa si hubiese existido.
- **Obtener el estado**: Debe devolver una lista con los estados en los que se encuentra cada surtidor.
- **Historial de suministros**: Los surtidores notificarán a la pista cuándo un suministro ha sido realizado y la pista debe ser conocedora de todos ellos y poderlos devolver a través de esta operación. En concreto, de cada suministro se debe conocer el surtidor que lo realizó, la fecha y hora de realización, el importe prefijado para el mismo si lo hubiese y el importe finalmente surtido. Este historial debe devolver los suministros ordenados por el importe suministrado (primeros los que mayor importe hayan suministrado). En caso de que dos suministros hayan suministrado el mismo importe, deberán devolverse antes aquellos que han sido más recientemente recibidos por la pista. Además, hay que tener presente que cuando un surtidor notifica la finalización de un suministro, debe quedarse bloqueado y sin importe prefijado alguno.

## Features

- Manage gas pumps (surtidores)
- Block and free gas pumps
- Set a maximum fill price for each gas pump
- Simulate the process of filling a gas pump
- Tests to ensure the correct behavior of the library

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 8.0 or later
- JetBrains Rider, Visual Studio 2022 or any other compatible IDE

### Installing

1. Clone the repository
2. Open the solution in JetBrains Rider
3. Build the solution

## Running the tests

The tests can be run directly from Visual Studio Code 2022 or the JetBrains Rider IDE.

## Built With

- C#
- NUnit for testing

## Authors

- Manuel Peña Llull