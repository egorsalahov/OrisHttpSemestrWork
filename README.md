# Mini HTTP Server

Простой HTTP-сервер для запуска веб-приложений и API.

## Что это такое

Этот сервер умеет:
- Обрабатывать API запросы через специальные классы-эндпоинты
- Автоматически определять тип контента для ответов
- Работать с формами и данными от пользователя

## Как запустить

1. Настройте параметры сервера в файле настроек (домен и порт)
2. Запустите программу
3. Сервер начнет работать на указанном адресе, например: http://localhost:8080/

## Как использовать

### Статические файлы
Положите ваши веб-страницы, стили и скрипты в папку Public. Сервер автоматически будет их раздавать:
- Главная страница: `/` → `Public/index.html`
- CSS файлы: `/style.css` → `Public/style.css`
- Изображения: `/images/photo.jpg` → `Public/images/photo.jpg`

### API эндпоинты
Создавайте специальные классы для обработки запросов. Сервер автоматически найдет их и подключит.

Примеры эндпоинтов:
- `GET /api/users` - получить список пользователей
- `POST /api/login` - авторизация с логином и паролем
- `GET /api/products` - получить товары

Эндпоинты могут возвращать:
- HTML страницы с данными
- JSON ответы для API
- Простой текст

### Работа с формами
При отправке форм данные автоматически преобразуются в параметры методов. Например, форма с полями "email" и "password" превратится в параметры для метода эндпоинта.

## Как остановить

Введите команду `/stop` в консоли, где запущен сервер.

## Что видит администратор

В консоли отображаются все запросы к серверу с указанием:
- Какой файл или эндпоинт был запрошен
- Статус обработки (успех или ошибка)
- Код ответа (200, 404 и т.д.)

Сервер готов к работе сразу после запуска и не требует сложной настройки.

Скрипты для создания базы данных

Для корректной работы сервера необходимо создать и наполнить структуру базы данных `tours_db` с помощью следующих SQL-команд.

### 1. Создание базы данных

- Выполните эту команду, чтобы создать базу данных (требуется подключение к базе, которая позволяет создавать новые базы, например, `postgres`):
    
SQL

```
CREATE DATABASE tours_db WITH ENCODING 'UTF8';
```

### 2. Создание таблиц и связей

- Подключитесь к только что созданной базе данных `tours_db` и выполните **весь** следующий SQL-скрипт.
    
SQL

```
-- Таблица Users
CREATE TABLE Users (
    Id SERIAL PRIMARY KEY,
    Login VARCHAR(100) NOT NULL UNIQUE, 
    PasswordInHash VARCHAR(255) NOT NULL,
    Permission BOOLEAN NOT NULL DEFAULT FALSE 
);


-- Таблица Contacts

CREATE TABLE Contacts (
    Id SERIAL PRIMARY KEY,
    phone_number VARCHAR(20) NOT NULL,
    contact_name VARCHAR(100) NOT NULL,
    email VARCHAR(100)
);


-- Таблица Legal_Info

CREATE TABLE Legal_Info (
    Id SERIAL PRIMARY KEY,
    registry_entry VARCHAR(50) NOT NULL,
    company_name VARCHAR(150) NOT NULL,
    insurance_info VARCHAR(200)
);


-- Таблица Hotels

CREATE TABLE Hotels (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    short_description TEXT, 
    Address VARCHAR(200) NOT NULL
);




-- Таблица Sessions (связь с Users)

CREATE TABLE Sessions (
    Token VARCHAR(36) PRIMARY KEY, 
    UserId INTEGER NOT NULL REFERENCES Users(Id),
    ExpiresAt TIMESTAMP WITH TIME ZONE NOT NULL,
    CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Таблица Tours (связь с Hotels, Contacts, Legal_Info)

CREATE TABLE Tours (
    Id SERIAL PRIMARY KEY,
    country VARCHAR(50) NOT NULL,
    city VARCHAR(50) NOT NULL,
    stars INTEGER NOT NULL,
    price INTEGER NOT NULL,
    hotel_id INTEGER NOT NULL REFERENCES Hotels(Id),
    contact_id INTEGER NOT NULL REFERENCES Contacts(Id),
    legal_info_id INTEGER NOT NULL REFERENCES Legal_Info(Id),
    image_path TEXT
);
```
