
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

Некоторые элементы UI представлены в виде виджетов (Widgets), они не следуют паттерну MVP и содержат в себе простую логику (CheckBoxWidget, CloseButtonWidget).
Подписка на события элементов View осуществляется с помощью паттерна Observable в презентерах.



## GamePlay
Здесь находятся Monobehavior компоненты сцены: PlayerInput, View компоненты игровых объектов (например BuildingView - открывает здание при покупке, анимирует прогресс и агрэйды).
SceneService, который является мостом между моделью и сценой, реализован в этом слое.


## Infrastructure
Тут расположены сервисы связанные с загрузкой префабов, сохранением состояния игры и заглушка в виде сервиса Web запросов.

## Application
Содержит точку входа в игру AppBoot.

## CompositionRoot
GameScope.cs  - регистрация зависимостей, реализована с помощью DI контейнера [VContainer](https://vcontainer.hadashikick.jp/)



