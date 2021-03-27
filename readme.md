<p align="center">
  <img src="https://user-images.githubusercontent.com/17460456/112715971-ac5ef200-8ef4-11eb-97d0-0c0ce6ce02b2.png">
</p>

_PopugJira - is a task tracker with additional accounting and analytics utilities_

_Created as education project, showing my capabilities in building asynchronous microservice architecture  
If you want to see project evolution please go to [closed pull requests](https://github.com/picolino/PopugJira/pulls?q=is%3Apr+is%3Aclosed)_

_Many thanks to authors of the [course about asynchronous architecture](https://education.borshev.com/architecture): @f213, @davydovanton_

<details>
 <summary>Project description & requirements (russian language only)</summary>

## Контекст и проблема

Топ-менеджмент UberPopug Inc столкнулся с проблемой производительности сотрудников. Чтобы повысить производительность, было принято решение выкинуть текущий таск-трекер и написать особый PopugJira, который должен будет увеличить производительность сотрудников на неопределенный процент. Чтобы попуги развивались и изучали новые направления, была придумана инновационная схема ассайна каждой задачи на случайного сотрудника. А для повышения мотивации топ-менеджмент решил сделать корпоративный аккаунтинг в таск-трекере, чтобы по количеству выполненных задач выплачивать сотрудникам зарплату. При этом задачи оцениваются с плавающим коэффициентом (местами отрицательным).

## Общие требования

В ходе обсуждения задачи с топ-менеджментом были выявлены следующие требования:

### Таск-трекер

1. Таск-трекер должен быть отдельным дашбордом и доступен всем сотрудникам компании UberPopug Inc.
2. Авторизация в таск-трекере должна выполняться через общий сервис авторизации UberPopug Inc (у нас там инновационная система авторизации на основе формы клюва).
3. В таск-трекере должны быть только задачи. Проектов, скоупов и спринтов никому не надо, ибо минимализм.
4. В таск-трекере новые таски может создавать кто угодно. У задачи должны быть описание, статус (выполнена или нет) и попуг, на которого заассайнена задача.
5. Менеджеры или администраторы должны иметь кнопку «заассайнить задачи», которая возьмет все открытые задачи и рандомно заассайнит каждую на любого из сотрудников. Не успел закрыть задачу до реассайна — сорян, делай следующую.
    1. **Дополнение:** Ассайнить задачу можно на кого угодно, это может быть любой аккаунт из системы.
    2. **Дополнение:** Ассайнить задачу можно только кнопкой «заассайнить задачи»
    3. **Дополнение:** при нажатии кнопки «заассайнить задачи» все текущие не закрытые задачи должны быть случайным образом перетасованы между каждым аккаунтом в системе
    4. **Дополнение:** мы не заморачиваемся на ограничение по нажатию на кнопку «заассайнить задачи». Ее можно нажимать хоть каждую секунду.
    5. **Дополнение:** на одного сотрудника может выпасть любое количество новых задач, может выпасть нуль, а может и 10.
6. Каждый сотрудник должен иметь возможность видеть в отдельном месте список заассайненных на него задач + отметить задачу выполненной.
7. После ассайна новой задачи сотруднику должно приходить оповещение на почту, в слак и в смс.

### Аккаунтинг: кто сколько денег заработал

1. Аккаунтинг должен быть в отдельном дашборде и доступным только для администраторов и бухгалтеров.
2. Авторизация в таск-трекере должна выполняться через общий сервис аунтификации UberPopug Inc.
3. У каждого из сотрудников должен быть свой счет, который показывает, сколько за сегодня он получил денег. У счета должен быть аудитлог того, за что были списаны или начислены деньги, с подробным описанием каждой из задач.
4. Расценки:
    - **Дополнение:** цена на задачу определяется единоразово, в момент ее появления в системе (можно с минимальной задержкой)
    - у сотрудника появилась новая задача — `rand(-10..-20)$`
    - сотрудник выполнил задачу — `rand(20..40)$`
    - **Дополнение:** деньги списываются сразу после ассайна на сотрудника, а начисляются после выполнения задачи.
    - **Дополнение:** отрицательный баланс переносится на следующий день. Единственный способ его погасить - закрыть достаточное количество задач в течении дня.
5. Вверху выводить количество заработанных топ менеджером за сегодня денег.
    1. **Дополнение:** т.е. сумма всех закрытых и созданных задач за день с противоположным знаком: `(sum(completed task amount) + sum(created task fee)) * -1`
6. В конце дня необходимо считать, сколько денег сотрудник получил за рабочий день, слать на почту сумму выплаты.
7. После выплаты баланса (в конце дня) он должен обнуляться и в аудитлоге должно быть отображено, что была выплачена сумма.
8. Дашборд должен выводить информацию по дням, а не за весь период сразу.
    1. изначально хватит только за сегодня. если чувствуете, что успеете сделать аналитику за каждый день недели - будет круто

### Аналитика

1. Аналитика — это отдельный дашборд, доступный только админам.
2. Нужно указывать, сколько заработал топ-менеджмент за сегодня: сколько попугов ушло в минус.
3. Нужно показывать самую дорогую задачу за: день, неделю и месяц.
    1. **Дополнение:** самой дорогой задачей является задача с наивысшей ценной из списка всех закрытых задач за определенный период времени
    2. **Дополнение:** пример того, как это может выглядеть:

        03.03 - самая дорогая задача - 28$
        02.03 - самая дорогая задача - 38$
        01.03 - самая дорогая задача - 23$
        01-03 марта - самая дорогая задача - 38$

## Технические дополнения

1. Никакого сложного UI-дизайна не надо, хватит самого банального бутстрапа или чистого html.
2. Нотификации достаточно вывести в консоль, что они произошли. Ничего делать не нужно.
3. Никакого реалтайма тоже не нужно, хватит рефреша страницы.
4. Язык и технологии можно выбрать любые.
5. **Дополнение:** Оповещения делать обычным выводом в STDOUT терминала, реальной логики отправки сообщений на почту, в слак или еще куда-то быть не должно.
6. Все права на PopugJira принадлежат UberPopug Inc.
</details>

## Architecture diagram
![image](https://user-images.githubusercontent.com/17460456/112715488-7d934c80-8ef1-11eb-9710-e4cef5e8913e.png)

## Microservices
### PopugJira.Identity
IdentityServer4 with ASP.NET Identity authorization service.

Contains registration and signing logic. Using OpenID through oAuth with Jwt.

### PopugJira.GoalTracker
ASP.NET 5 WebApi service.

Used for creating/updating goals for users.

### PopugJira.Accounting
ASP.NET 5 WebApi service.

Used for payment processing to users

### PopugJira.Analytics
ASP.NET 5 WebApi service.

User for provide analytics information

### PopugJira.Notifications
ASP.NET 5 WebApi service

User for notify users about some actions in the system

### PopugJira
Blazor WebAssembly application

Used as client application for user's browser. Communicates with all microservices

### Other
**PopugJira.Microservice** - library, contains base logic for every microservice. Just for simplify building new microservices.

**PopugJira.AutoDI** - library, contains logic for automatic dependency injection.

**PopugJira.Common** - library, contains other shared logic

**PopugJira.EventBus** - library, contains schema registry for events and base logic for event bus.

## Event bus
Each microservice uses shared RabbitMQ instance.

## Databases
Each microservice contains separated SQLite database and manipulates data only within that database.

All databases places into PopugJira (blazor) project folder during initialization of each service.

## Quick Start
1. Deploy shared RabbitMQ instance
1. Download and install .NET 5 SDK
1. Clone repository into any local folder
1. Change RabbitMQ connection strings into each microservice (`appsettings.json`)
1. Run all services (order of launch doesn't matter)
1. Navigate to `https://localhost:5001` or `http://localhost:5000`
1. Register few users (with different roles, note that not all roles has access to all parts of the system)
1. Login as any registered user
1. Enjoy
