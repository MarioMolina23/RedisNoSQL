from fastapi import APIRouter
from redis_client.crud import delete_hashaudif, get_hashaudif, get_hashAllaudif, save_hashaudif #Se importa en el FastAPi la clase llamada router
from schemas.audifonos import audifonos #importa routes computadoras


routes_audifonos = APIRouter() #Se crea variable ApiRouter

#guardamos un ejemplo para poder filtrarlo en buscar por ID
#pretende ser la DB
fake_db = [] 


@routes_audifonos.post("/create", response_model=audifonos) #Ruta create tipo POST/agregar
def create(audif: audifonos): #variable es compu de tipo computadoras
    try:
        #operacion DB 
        fake_db.append(audif.dict())

        #operacion cache para guardar en Redis, convertimos los datos a diccionarios con dict()
        save_hashaudif(key=audif.dict()["id"], data=audif.dict())

        return audif
    except Exception as e:
        return e

@routes_audifonos.get("/get/{id}")
def get(id: str):
    try:
        #operacion cache
        data = get_hashaudif(key=id)
        if len(data) == 0: #si es igual a 0 es porque no hay nada en la DB
            #operacion DB
            audif = list(filter(lambda field: field["id"] == id, fake_db))[0]#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID en el indice 0 de la lista
            
            #operacion cache
            save_hashaudif(key=id, data=audif)

            return audif# si no existe en redis devolvemos de memoria
        return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_audifonos.get("/getAll")
def get():
    try:
        #operacion cache
        data = get_hashAllaudif()
        if len(data) != 0: #si es diferente a 0 es porque hay data en la DB
            return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_audifonos.delete("/delete/{id}")
def get(id: str):
    try:
        keyz = audifonos.__fields__.keys() #esto devuelve un array con c/u de las claves
        #operacion cache
        delete_hashaudif(key=id, keys=keyz)

        #operacion DB
        audif = list(filter(lambda field: field["id"] != id, fake_db))#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID 
        if len(audif) != 0: #si la longitud de compu es mayor a 0  hay datos
            fake_db.remove(audif)

        return{
            "message": "success"
        }
    except Exception as e:
        return e