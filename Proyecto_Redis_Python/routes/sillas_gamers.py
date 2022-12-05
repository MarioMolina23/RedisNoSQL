from fastapi import APIRouter
from redis_client.crud import delete_hashsillasg, get_hashsillasg, get_hashAllsillasg, save_hashsillasg #Se importa en el FastAPi la clase llamada router
from schemas.sillas_gamers import sillas_gamers #importa routes computadoras


routes_sillas_gamers = APIRouter() #Se crea variable ApiRouter

#guardamos un ejemplo para poder filtrarlo en buscar por ID
#pretende ser la DB
fake_db = [] 


@routes_sillas_gamers.post("/create", response_model=sillas_gamers) #Ruta create tipo POST/agregar
def create(sillasg: sillas_gamers): #variable es compu de tipo computadoras
    try:
        #operacion DB 
        fake_db.append(sillasg.dict())

        #operacion cache para guardar en Redis, convertimos los datos a diccionarios con dict()
        save_hashsillasg(key=sillasg.dict()["id"], data=sillasg.dict())

        return sillasg
    except Exception as e:
        return e

@routes_sillas_gamers.get("/get/{id}")
def get(id: str):
    try:
        #operacion cache
        data = get_hashsillasg(key=id)
        if len(data) == 0: #si es igual a 0 es porque no hay nada en la DB
            #operacion DB
            sillasg = list(filter(lambda field: field["id"] == id, fake_db))[0]#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID en el indice 0 de la lista
            
            #operacion cache
            save_hashsillasg(key=id, data=sillasg)

            return sillasg# si no existe en redis devolvemos de memoria
        return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_sillas_gamers.get("/getAll")
def get():
    try:
        #operacion cache
        data = get_hashAllsillasg()
        if len(data) != 0: #si es diferente a 0 es porque hay data en la DB
            return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_sillas_gamers.delete("/delete/{id}")
def get(id: str):
    try:
        keyz = sillas_gamers.__fields__.keys() #esto devuelve un array con c/u de las claves
        #operacion cache
        delete_hashsillasg(key=id, keys=keyz)

        #operacion DB
        sillasg = list(filter(lambda field: field["id"] != id, fake_db))#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID 
        if len(sillasg) != 0: #si la longitud de compu es mayor a 0  hay datos
            fake_db.remove(sillasg)

        return{
            "message": "success"
        }
    except Exception as e:
        return e