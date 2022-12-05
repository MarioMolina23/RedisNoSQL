from fastapi import APIRouter
from redis_client.crud import delete_hashparla, get_hashparla, get_hashAllparla, save_hashparla #Se importa en el FastAPi la clase llamada router
from schemas.parlantes import parlantes #importa routes computadoras


routes_parlantes = APIRouter() #Se crea variable ApiRouter

#guardamos un ejemplo para poder filtrarlo en buscar por ID
#pretende ser la DB
fake_db = [] 


@routes_parlantes.post("/create", response_model=parlantes) #Ruta create tipo POST/agregar
def create(parla: parlantes): #variable es compu de tipo computadoras
    try:
        #operacion DB 
        fake_db.append(parla.dict())

        #operacion cache para guardar en Redis, convertimos los datos a diccionarios con dict()
        save_hashparla(key=parla.dict()["id"], data=parla.dict())

        return parla
    except Exception as e:
        return e

@routes_parlantes.get("/get/{id}")
def get(id: str):
    try:
        #operacion cache
        data = get_hashparla(key=id)
        if len(data) == 0: #si es igual a 0 es porque no hay nada en la DB
            #operacion DB
            parla = list(filter(lambda field: field["id"] == id, fake_db))[0]#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID en el indice 0 de la lista
            
            #operacion cache
            save_hashparla(key=id, data=parla)

            return parla# si no existe en redis devolvemos de memoria
        return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_parlantes.get("/getAll")
def get():
    try:
        #operacion cache
        data = get_hashAllparla()
        if len(data) != 0: #si es diferente a 0 es porque hay data en la DB
            return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_parlantes.delete("/delete/{id}")
def get(id: str):
    try:
        keyz = parlantes.__fields__.keys() #esto devuelve un array con c/u de las claves
        #operacion cache
        delete_hashparla(key=id, keys=keyz)

        #operacion DB
        parla = list(filter(lambda field: field["id"] != id, fake_db))#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID 
        if len(parla) != 0: #si la longitud de compu es mayor a 0  hay datos
            fake_db.remove(parla)

        return{
            "message": "success"
        }
    except Exception as e:
        return e