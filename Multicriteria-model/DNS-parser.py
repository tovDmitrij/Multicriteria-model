from bs4 import BeautifulSoup
from selenium import webdriver
import time
import pyodbc
webdriver = webdriver.Chrome("C:/Users/Дмитрий/Desktop/Multicriteria-model/Multicriteria-model/chromedriver.exe")

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
            case "Процессор":
                product_Name = items[product][items[product].find(' ') + 1 : items[product].find('[') - 1]
                product_Cores = items[product][items[product].find(',') + 2 : items[product].find('x') - 1]
                product_Frequency = items[product][items[product].find('x') + 2 : items[product].find("ГГц") - 1]
                product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
                requestString = f"insert into Processors(Product_Name, Cores, Frequency, Price) values('{product_Name}', '{product_Cores}', '{product_Frequency}', '{product_Price}');"
            case "Оперативная":
                product_Name = items[product][items[product].find(' ', items[product].find(' ') + 1) : items[product].find('[') - 1]
                product_Memory = items[product][items[product].find('[D') - 6 :  items[product].find("ГБ") - 1].replace(' ', '')
                product_Frequency = items[product][items[product].find('шт,') + 4 : items[product].find('МГц') - 1]
                product_Price = items[product][items[product].rfind(']') + 2 : items[product].find('₽') - 1].replace(' ', '')
                requestString = f"insert into RAM(Product_Name, Memory, Frequency, Price) values('{product_Name}', '{product_Memory}', '{product_Frequency}', '{product_Price}');"
        goodsType = items[product][items[product].find('"') + 2 : items[product].find('р') + 1 ]
        if(goodsType == "Монитор"):
                product_Name = ...
                product_ScreenSize = ...
                product_Frequency = ...
                product_Price = ...
                requestString = ...
        goodsType = items[product][items[product].find('Б') + 2 : items[product].find('ск') + 2]
        if(goodsType == "Жесткий диск"):
                print("do sth...")
        try:
            dbcursor.execute(requestString)
            connection.commit()
        except:
            continue;

# 23.8" Монитор Dell S2421HN белый [1920x1080@75 Гц, IPS, 4 мс, 1000 : 1, 250 Кд/м², 178°/178°, HDMI, AMD FreeSync] 16 499 ₽
# 27" Монитор MSI Optix G27C4 черный [1920x1080@165 Гц, VA, 1 мс, 3000 : 1, 250 Кд/м², 178°/178°, DisplayPort, HDMI, изогнутый, AMD FreeSync Premium] 23 999 ₽
# 24.5" Монитор Dell S2522HG черный [1920x1080@240 Гц, IPS, 1 мс, 1000 : 1, 400 Кд/м², 178°/178°, HDMI x2, DisplayPort, USB х2 шт, AMD FreeSync Premium, NVIDIA G-SYNC Compatible] 27 999 ₽
# 23.6" Монитор ASUS TUF Gaming VG24VQE черный [1920x1080@165 Гц, VA, 1 мс, 3000 : 1, 250 Кд/м², 178°/178°, DisplayPort, HDMI, изогнутый, AMD FreeSync Premium] 19 999 ₽
# 27" Монитор HP 27mq черный [2560x1440@60 Гц, IPS, 5 мс, 1000 : 1, 300 Кд/м², 178°/178°, HDMI, VGA (D-Sub)] 23 999 ₽
# 31.5" Монитор Samsung U32J590UQI черный [3840x2160@60 Гц, VA, 4 мс, 3000 : 1, 270 Кд/м², 178°/178°, DisplayPort, HDMI, AMD FreeSync] 33 499 ₽

# 0.5 ТБ Жесткий диск WD Black [WD5000LPSX] [SATA III, 7200 rpm, кэш память - 64 МБ] 4 199 ₽
# 0.5 ТБ Жесткий диск WD Blue [WD5000LPZX] [SATA III, 5400 rpm, 150 Мбайт/сек, кэш память - 128 МБ] 3 499 ₽
# 500 ГБ Жесткий диск Seagate BarraCuda [ST500LM030] [SATA III, 5400 rpm, 140 Мбайт/с, кэш память - 128 Мб] 3 299 ₽
# 500 ГБ Жесткий диск Seagate BarraCuda Pro [ST500LM034] [SATA III, 7200 rpm, 160 Мбайт/с, кэш память - 128 Мб] 4 599 ₽
# 1 ТБ Жесткий диск Seagate BarraCuda Pro [ST1000LM049] [SATA III, 7200 rpm, 160 Мбайт/сек, кэш память - 128 МБ] 7 999 ₽
# 500 ГБ Жесткий диск Toshiba L200 Slim [HDWK105UZSVA] [SATA III, 5400 rpm, кэш память - 8 Мб] 3 499 ₽

# currentData = {
#     "Processor": {
#     "Name": product_Name,
#     "Species": {
#         "Core": product_Cores,
#         "Frequency": product_Frequency
#     },
#     "Price": product_Price
#     }
# }
# data.append(currentData)
# with open("data_file.json", "w") as write_file:
#     json.dump(data, write_file)



# name = 'Видеокарты'
# items = GetProducts(name)
# InsertIntoDataBase(items)

# name = 'Процессоры'
# items = GetProducts(name)
# InsertIntoDataBase(items)

# name = 'Оперативная память'
# items = GetProducts(name)
# InsertIntoDataBase(items)

# name = 'Монитор'
# items = GetProducts(name)
# InsertIntoDataBase(items)

name = 'Жесткий диск'
items = GetProducts(name)
InsertIntoDataBase(items)

for item in items:
    print(item)
print(f"\n\nКол-во товаров: {len(items)}")