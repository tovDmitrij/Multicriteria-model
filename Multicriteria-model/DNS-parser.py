from bs4 import BeautifulSoup
from selenium import webdriver
import time
import pyodbc
webdriver = webdriver.Chrome("chromedriver.exe")

def GetProducts(search: str) -> list:
    url = f'https://www.dns-shop.ru/search/?q={search}&stock=now-today-tomorrow-later&p=1'
    webdriver.get(url)
    try:
        while(True):
            nextPageButton = webdriver.find_element_by_class_name('pagination-widget__show-more-btn')
            webdriver.execute_script("arguments[0].click();",nextPageButton)
            time.sleep(1)
    except:
        print("Goods out of stock!")
    soup = BeautifulSoup(webdriver.page_source,'lxml')
    goods = soup.find_all('div', 'catalog-product ui-button-widget')
    listOfGoods = []
    for product in range(len(goods)):
        try:
            listOfGoods.append(f"{goods[product].find('span').text} {goods[product].find('div', 'product-buy__price').text}")
        except:
            continue
    return listOfGoods
def InsertIntoDataBase(items: list):
    connectionString = (
    "Driver={SQL Server Native Client 11.0};"
    "Server=DESKTOP-R2N8P3B;"
    "Database=DB_Goods;"
    "Trusted_Connection=yes;"
    )
    connection = pyodbc.connect(connectionString, autocommit = True)
    dbcursor = connection.cursor()
    requestString = ""
    for product in range(len(items)):
        goodsType = items[product][0 : items[product].find(' ')]
        match goodsType:
            case "Видеокарта":
                product_Name = items[product][items[product].find(' ') + 1 : items[product].find('[') - 1]
                product_VideoMemory = items[product][items[product].find(',') + 2 : items[product].find("ГБ") - 1]
                product_Frequency = items[product][items[product].rfind(' ', 0, items[product].rfind("МГц") - 1) + 1 : items[product].rfind("МГц") - 1]
                product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
                requestString = f"insert into Videocards(Product_Name, Video_memory, Frequency, Price) values('{product_Name}', '{product_VideoMemory}', '{product_Frequency}', '{product_Price}');"
                try:
                    dbcursor.execute(requestString)
                except:
                    requestString = f"update Videocards set Price = '{product_Price}' where Product_Name = '{product_Name}'"
                    dbcursor.execute(requestString)
            case "Процессор":
                product_Name = items[product][items[product].find(' ') + 1 : items[product].find('[') - 1]
                product_Cores = items[product][items[product].find(',') + 2 : items[product].find('x') - 1]
                product_Frequency = items[product][items[product].find('x') + 2 : items[product].find("ГГц") - 1]
                product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
                requestString = f"insert into Processors(Product_Name, Cores, Frequency, Price) values('{product_Name}', '{product_Cores}', '{product_Frequency}', '{product_Price}');"
                try:
                    dbcursor.execute(requestString)
                except:
                    requestString = f"update Processors set Price = '{product_Price}' where Product_Name = '{product_Name}'"
                    dbcursor.execute(requestString)
            case "Оперативная":
                product_Name = items[product][items[product].find(' ', items[product].find(' ') + 1) : items[product].find('[') - 1]
                product_Memory = items[product][items[product].find('[D') - 6 :  items[product].find("ГБ") - 1].replace(' ', '')
                product_Frequency = items[product][items[product].find('шт,') + 4 : items[product].find('МГц') - 1]
                product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
                requestString = f"insert into RAM(Product_Name, Memory, Frequency, Price) values('{product_Name}', '{product_Memory}', '{product_Frequency}', '{product_Price}');"
                try:
                    dbcursor.execute(requestString)
                except:
                    requestString = f"update RAM set Price = '{product_Price}' where Product_Name = '{product_Name}'"
                    dbcursor.execute(requestString)
        goodsType = items[product][items[product].find('"') + 2 : items[product].find('р') + 1 ]
        if(goodsType == "Монитор"):
                product_Name = items[product][items[product].find('р') + 2 : items[product].find('[') - 1]
                product_ScreenSize = items[product][items[product].find('[') + 1 : items[product].find('@') - 1]
                product_Frequency = items[product][items[product].find('@') + 1 : items[product].find('Гц') - 1]
                product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')]
                requestString = f"insert into Monitors(Product_Name, ScreenSize, Frequency, Price) values('{product_Name}', '{product_ScreenSize}', '{product_Frequency}', '{product_Price}')"
                try:
                    dbcursor.execute(requestString)
                except:
                    requestString = f"update Monitors set Price = '{product_Price}' where Product_Name = '{product_Name}'"
                    dbcursor.execute(requestString)
        goodsType = items[product][items[product].find('Б') + 2 : items[product].find('ск') + 2]
        if(goodsType == "Жесткий диск"):
                product_Name = items[product][items[product].find('ск') + 2 : items[product].find('[') - 2]
                product_Memory = items[product][0 : items[product].find(' ') - 1]
                product_Speed = items[product][items[product].rfind(',', items[product].find('rpm')) + 2 : items[product].find('rpm') - 2]
                product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')]
                try:
                    dbcursor.execute(requestString)
                except:
                    requestString = f"update RAM set Price = '{product_Price}' where Product_Name = '{product_Name}'"
                    dbcursor.execute(requestString)
        connection.commit()
name = input()
items = GetProducts(name)
InsertIntoDataBase(items)
for item in items:
    print(item)
print(f"\n\nКол-во товаров: {len(items)}")
