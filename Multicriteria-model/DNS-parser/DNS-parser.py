from bs4 import BeautifulSoup
from selenium import webdriver
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.common.by import By
import time
import pyodbc

driver = webdriver.Chrome(ChromeDriverManager().install())
serverName = "Server Name"
dbName = "Goods"

#Получение списка товаров
def GetProducts(search: str) -> list:
    url = f'https://www.dns-shop.ru/search/?q={search}&stock=now-today-tomorrow-later&p=1'
    driver.get(url)
    try:
        while(True):
            nextPageButton = driver.find_element(By.CLASS_NAME, 'pagination-widget__show-more-btn')
            driver.execute_script("arguments[0].click();",nextPageButton)
            time.sleep(1)
    except:
        print("Goods out of stock!")
    soup = BeautifulSoup(driver.page_source,'lxml')
    goods = soup.find_all('div', 'catalog-product ui-button-widget')
    listOfGoods = []
    for product in range(len(goods)):
        try:
            listOfGoods.append(f"{goods[product].find('span').text} {goods[product].find('div', 'product-buy__price').text}")
        except:
            continue
    return listOfGoods

#Внедрение списка товаров в БД MSSQL
def InsertIntoDataBase(items: list):
    connectionString = f"Driver=SQL Server Native Client 11.0;Server={serverName};Database={dbName};Trusted_Connection=yes;"
    connection = pyodbc.connect(connectionString, autocommit = True)
    dbcursor = connection.cursor()
    requestString = ""
    for product in range(len(items)):
        product_Name = items[product][items[product].find(' ') + 1 : items[product].find('[') - 1]
        product_Cores = items[product][items[product].find(',') + 2 : items[product].find('x') - 1]
        product_Frequency = items[product][items[product].find('x') + 2 : items[product].find("ГГц") - 1]
        product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
        requestString = f"insert into Processor(Name, Cores, Frequency, Price) values('{product_Name}', '{product_Cores}', '{product_Frequency}', '{product_Price * -1}');"
        try:
            dbcursor.execute(requestString)
        except:
            requestString = f"update Processor set Price = '{product_Price * -1}' where Name = '{product_Name}'"
            dbcursor.execute(requestString)
        connection.commit()
name = input("Введите тип товара: ")
items = GetProducts(name)
InsertIntoDataBase(items)
for item in items:
    print(item)
print(f"\n\nКол-во товаров: {len(items)}")


#goodsType = items[product][0 : items[product].find(' ')]
#match goodsType:
#    case "Процессор":
#
#    case "Видеокарта":
#       product_Name = items[product][items[product].find(' ') + 1 : items[product].find('[') - 1]
#       product_VideoMemory = items[product][items[product].find(',') + 2 : items[product].find("ГБ") - 1]
#       product_Frequency = items[product][items[product].rfind(' ', 0, items[product].rfind("МГц") - 1) + 1 : items[product].rfind("МГц") - 1]
#       product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
#       requestString = f"insert into Videocards(Name, Video_memory, Frequency, Price) values('{product_Name}', '{product_VideoMemory}', '{product_Frequency}', '{product_Price}');"
#       try:
#            dbcursor.execute(requestString)
#       except:
#            requestString = f"update Videocards set Price = '{product_Price}' where Name = '{product_Name}'"
#            dbcursor.execute(requestString)
#    case "Оперативная":
#        product_Name = items[product][items[product].find(' ', items[product].find(' ') + 1) : items[product].find('[') - 1]
#        product_Memory = items[product][items[product].find('[D') - 6 :  items[product].find("ГБ") - 1].replace(' ', '')
#        product_Frequency = items[product][items[product].find('шт,') + 4 : items[product].find('МГц') - 1]
#        product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
#        requestString = f"insert into RAM(Name, Memory, Frequency, Price) values('{product_Name}', '{product_Memory}', '{product_Frequency}', '{product_Price}');"
#        try:
#            dbcursor.execute(requestString)
#        except:
#            requestString = f"update RAM set Price = '{product_Price}' where Name = '{product_Name}'"
#            dbcursor.execute(requestString)
#goodsType = items[product][items[product].find('"') + 2 : items[product].find('р') + 1 ]
#if(goodsType == "Монитор"):
#        product_Name = items[product][items[product].find('р') + 2 : items[product].find('[') - 1]
#        product_ScreenSize = items[product][items[product].find('[') + 1 : items[product].find('@')]
#        product_Frequency = items[product][items[product].find('@') + 1 : items[product].find('Гц') - 1]
#        product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
#        requestString = f"insert into Monitors(Name, ScreenSize, Frequency, Price) values('{product_Name}', '{product_ScreenSize}', '{product_Frequency}', '{product_Price}');"
#        try:
#            dbcursor.execute(requestString)
#        except:
#            requestString = f"update Monitors set Price = '{product_Price}' where Name = '{product_Name}'"
#            dbcursor.execute(requestString)
#goodsType = items[product][items[product].find('Б') + 2 : items[product].find('ск') + 2]
#if(goodsType == "Жесткий диск"):
#        product_Name = items[product][items[product].find('ск') + 2 : items[product].find('[') - 1]
#        product_Memory = items[product][0 : items[product].find(' ')]
#        if(float(product_Memory) < 20):
#            product_Memory = float(product_Memory) * 1024
#        # product_Speed = items[product][items[product].find('бит/с') + 7 : items[product].find('об/мин') - 1]
#        product_Speed = items[product][items[product].find('III') + 5 : items[product].find('rpm') - 1]
#        product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
#        requestString = f"insert into HDD(Name, Memory, Speed, Price) values('{product_Name}', '{product_Memory}', '{product_Speed}', '{product_Price}');"
#        try:
#            dbcursor.execute(requestString)
#        except:
#            requestString = f"update HDD set Price = '{product_Price}' where Name = '{product_Name}'"
#            dbcursor.execute(requestString)