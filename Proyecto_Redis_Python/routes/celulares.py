from fastapi import APIRouter
from redis_client.crud import delete_hashcelul, get_hashcelul, get_hashAllcelul, save_hashcelul #Se importa en el FastAPi la clase llamada router
from schemas.celulares import celulares #importa routes computadoras


routes_celulares = APIRouter() #Se crea variable ApiRouter

#guardamos un ejemplo para poder filtrarlo en buscar por ID
#pretende ser la DB
fake_db = [] 


@routes_celulares.post("/create", response_model=celulares) #Ruta create tipo POST/agregar
def create(celul: celulares): #variable es compu de tipo computadoras
    try:
        #operacion DB 
        fake_db.append(celul.dict())

        #operacion cache para guardar en Redis, convertimos los datos a diccionarios con dict()
        save_hashcelul(key=celul.dict()["id"], data=celul.dict())

        return celul
    except Exception as e:
        return e

@routes_celulares.get("/get/{id}")
def get(id: str):
    try:
        #operacion cache
        data = get_hashcelul(key=id)
        if len(data) == 0: #si es igual a 0 es porque no hay nada en la DB
            #operacion DB
            celul = list(filter(lambda field: field["id"] == id, fake_db))[0]#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID en el indice 0 de la lista
            
            #operacion cache
            save_hashcelul(key=id, data=celul)

            return celul# si no existe en redis devolvemos de memoria
        return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_celulares.get("/getAll")
def get():
    try:
        #operacion cache
        data = get_hashAllcelul()
        if len(data) != 0: #si es diferente a 0 es porque hay data en la DB
            return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_celulares.delete("/delete/{id}")
def get(id: str):
    try:
        keyz = celulares.__fields__.keys() #esto devuelve un array con c/u de las claves
        #operacion cache
        delete_hashcelul(key=id, keys=keyz)

        #operacion DB
        celul = list(filter(lambda field: field["id"] != id, fake_db))#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID 
        if len(celul) != 0: #si la longitud de compu es mayor a 0  hay datos
            fake_db.remove(celul)

        return{
            "message": "success"
        }
    except Exception as e:
        return e