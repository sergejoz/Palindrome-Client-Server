﻿# Клиент-сервер приложение для определения палиндромов. 

Делал на базе .Net 4.7

## Клиент
* Клиент и сервер взаимодействуют по сети
* Клиент при запуске принимает путь до директории с входными данными
* Входные данные имеют вид текстовых файлов произвольной длины
* Файлов может быть от 0 до 127
* Задача клиента отправить текст каждого файла на сервер для проверки и получить ответ в формате True/False
* Полученные ответы Клиент может предоставить в любом человекочитаемом  формате, например: вывести в консоль (или GUI) в ходе работы приложения, сохранить в файл по итогам работы приложения (**файл сохраняется в \ClientServer\ClientServer\bin\Debug\log.txt**).
- Клиент успешно решил поставленную перед ним задачу, если в выводе есть корректные ответы от Сервера по каждому файлу.
- Клиент знает о том, что Сервер может обрабатывать несколько запросов за раз, но не знает сколько конкретно. И стремится использовать возможности сервера максимально.

## Сервер
* Сервер и клиент взаимодействуют по сети
* Сервер принимает входящие запросы от Клиента и проверяет является ли присланный текст палиндромом. Если текст является палиндромом, то Сервер возвращает ответ True, иначе False
* Так как вычисление палиндрома задача для Сервера творческая, то он никак не может затратить на нее меньше 1 секунды (допустимо использование методов Thread.Sleep или Task.Delay)
* Сервер может обрабатывать за раз только N запросов, и в случае если свободных потоков для обработки нет, вернуть клиенту ошибку. N - является целым числом от 1 до 10 и **задается на форме сервера**

## Дополнение
* Проверено при работе внутри домашней сети
* Серверу передают только текст, возвращает текст и ответ
* После завершения работы программы лог работы сохраняется в \ClientServer\ClientServer\bin\Debug\log.txt
* Количество потоков задается на форме сервера для удобства
