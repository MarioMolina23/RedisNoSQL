from fastapi import APIRouter
from redis_client.crud import delete_hashestuc, get_hashestuc, get_hashAllestuc, save_hashestuc #Se importa en el FastAPi la clase llamada router
from schemas.estuches import estuches #importa routes computadoras


routes_estuches = APIRouter() #Se crea variable ApiRouter

#guardamos un ejemplo para poder filtrarlo en buscar por ID
#pretende ser la DB
fake_db = [] 


@routes_estuches.post("/create", response_model=estuches) #Ruta create tipo POST/agregar
def create(estuc: estuches): #variable es compu de tipo computadoras
    try:
        #operacion DB 
        fake_db.append(estuc.dict())

        #operacion cache para guardar en Redis, convertimos los datos a diccionarios con dict()
        save_hashestuc(key=estuc.dict()["id"], data=estuc.dict())

        return estuc
    except Exception as e:
        return e

@routes_estuches.get("/get/{id}")
def get(id: str):
    try:
        #operacion cache
        data = get_hashestuc(key=id)
        if len(data) == 0: #si es igual a 0 es porque no hay nada en la DB
            #operacion DB
            estuc = list(filter(lambda field: field["id"] == id, fake_db))[0]#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID en el indice 0 de la lista
            
            #operacion cache
            save_hashestuc(key=id, data=estuc)

            return estuc# si no existe en redis devolvemos de memoria
        return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_estuches.get("/getAll")
def get():
    try:
        #operacion cache
        data = get_hashAllestuc()
        if len(data) != 0: #si es diferente a 0 es porque hay data en la DB
            return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_estuches.delete("/delete/{id}")
def get(id: str):
    try:
        keyz = estuches.__fields__.keys() #esto devuelve un array con c/u de las claves
        #operacion cache
        delete_hashestuc(key=id, keys=keyz)

        #operacion DB
        estuc = list(filter(lambda field: field["id"] != id, fake_db))#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID 
        if len(estuc) != 0: #si la longitud de compu es mayor a 0  hay datos
            fake_db.remove(estuc)

        return{
            "message": "success"
        }
    except Exception as e:
        return e