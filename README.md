# KittenApi_Example v1.0
Данный кроссплатформенный api сервис предназначен для работы с информацией о животных ("котики").
Проект включает два сервиса:

1. KittenApi, используется для основной работы с животными и разделена условно на три части.

  Методы для работы с информацией о клиниках (Clinic):
   - Регистрация в базе данных;
   - Добавление в животного в клинику;
   - Чтение информации из базы данных о клинике;
   - Редактирование названия клиники по id;
   - Удаление информации о клинике по id из базы данных.
  Методы для работы с информацией о животных (Kitten):
   - Регистрация животного в базе данных;
   - Чтение информации о животном по кличке;
   - Редактирование информации о животном по id;
   - Удаление информации о животном из базы данных по id.
  Метод для добавления "медицинской процедуры" животному (MedicalServices):
   - Добавление названия процедуры по id животного.

"Clinic"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/Clinic.png "Clinic")

"Kittens"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/Kittens.png "Kittens")

"MedicalServices"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/MedicalServices.png "MedicalServices")

2. AuthApi, используется для авторизации пользователя.

"AuthApi"
![alt tag](https://github.com/AlexanderMeshchaninov/Screenshots/blob/main/AuthApi.png "AuthApi")
  Методы для работы с авторизацией пользователя:
   - Регистрация пользователя (указывается логин и пароль);
   - Авторизация пользователя с получением токена доступа;
   - Получение обновленного токена (refreshToken).

В проект также добавлены:
 - Юнит тесты;
 - Логирование;
 - Mapper;
 - FluentValidation;
 - PostgresSql;
 - Hosted service миграции бд для авторизации пользователей;
 - Hosted service миграции бд для животных;
 - Docker-compose file.
