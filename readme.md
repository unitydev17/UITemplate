
# UITemplate

### Шаблон игрового проекта с использованием Onion архитектуры и MVP паттерна для UI

![Onion architecture](/Assets/_Project/Images/readme/onion.png)

## Core (domain)
Слой содержит в себе логику и управление состоянием игры. 
Является моделью приложения и не содержит прямых ссылок на UI и Monobehavior компоненты игры.
Обменивается управляющими сообщениями с UI и компонентами игровых объектов сцены.
Для подписки/отправления сообщений используется MessageBroker [UniRx](https://github.com/neuecc/UniRx)

Имеет возможность обновлять игровые компоненты на сцене через посредника в виде SceneService.
Например модель игры содержит список условных зданий, которым на сцене соответствуют игровые объекты. Для обновления их состояния используется этот сервис (активация, апгрэйды)



## UI
Реализован используя паттерн MVP. Используется два вида окон - полноэкранное (HUD) и всплывающие попапы.
UIManager.cs отвечает за создание, открытие и закрытие попапов и окон.
Окна в виде префабов находятся в локальном Addressable билде и подгружаются асинхронно с помощью async/await [UniTask](https://github.com/Cysharp/UniTask).
Каждый префаб окна имеет свой Canvas для большей автономности.
После закрытие окно дестроится.






