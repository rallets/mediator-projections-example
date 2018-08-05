# mediator-projections-example

This project shows how to use MediatoR, Autofac, Automapper, and FluentValidation to build an event system used to generate projections. Main goal is the Single Responsibility. This pattern can be used in any type of project, from RESTful WebApi to console applications.

The console application raise a CreateClient event, if the validation passes, the main event is handled (it stores a list of Clients with and their GUIDs) and return the result object. 

Then two more events handler are executed: the 1st stores a list of ordered GUIDs, the second stores the total number of clients.

The stores are text files, to easily run the project and inspect the output. 

In real example they could be read-model tables in one or more databases, to implement CQRS or CQS pattern. The event handlers could react from or send messages to a message bus, like RabbitMQ or Azure Message Bus.

## Used components

* MediatoR: [Mediator](https://en.wikipedia.org/wiki/Mediator_pattern)'s implementation, allows to implement easily a event-based data flow in C#. It has more powerful functionality not shown in this basic example.

* Autofac: implements the DI/IoC

* Automapper: allows to trasform objects easily. Can be used to calculate/generate projections too.

* FluentValidaion: allows to write validators in our event flow.

#### References:
* [MediatoR](https://github.com/jbogard/MediatR)
* [Autofac](https://github.com/autofac/Autofac)
* [Automapper](https://github.com/AutoMapper/AutoMapper)
* [FluentValidation](https://github.com/JeremySkinner/FluentValidation)
