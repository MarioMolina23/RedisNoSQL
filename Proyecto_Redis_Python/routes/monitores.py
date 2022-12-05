from fastapi import APIRouter
from redis_client.crud import delete_hashmonit, get_hashmonit, save_hashAllmonit, save_hashmonit #Se importa en el FastAPi la clase llamada router
from schemas.monitores import monitores #importa routes computadoras


routes_monitores = APIRouter() #Se crea variable ApiRouter

#guardamos un ejemplo para poder filtrarlo en buscar por ID
#pretende ser la DB
fake_db = [] 


@routes_monitores.post("/create", response_model=monitores) #Ruta create tipo POST/agregar
def create(monit: monitores): #variable es compu de tipo computadoras
    try:
        #operacion DB 
        fake_db.append(monit.dict())

        #operacion cache para guardar en Redis, convertimos los datos a diccionarios con dict()
        save_hashmonit(key=monit.dict()["id"], data=monit.dict())

        return monit
    except Exception as e:
        return e

@routes_monitores.get("/get/{id}")
def get(id: str):
    try:
        #operacion cache
        data = get_hashmonit(key=id)
        if len(data) == 0: #si es igual a 0 es porque no hay nada en la DB
            #operacion DB
            monit = list(filter(lambda field: field["id"] == id, fake_db))[0]#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID en el indice 0 de la lista
            
            #operacion cache
            save_hashmonit(key=id, data=monit)

            return monit# si no existe en redis devolvemos de memoria
        return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_monitores.get("/getAll")
def get():
    try:
        #operacion cache
        data = save_hashAllmonit()
        if len(data) != 0: #si es diferente a 0 es porque hay data en la DB
            return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_monitores.delete("/delete/{id}")
def get(id: str):
    try:
        keyz = monitores.__fields__.keys() #esto devuelve un array con c/u de las claves
        #operacion cache
        delete_hashmonit(key=id, keys=keyz)

        #operacion DB
        monit = list(filter(lambda field: field["id"] != id, fake_db))#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID 
        if len(monit) != 0: #si la longitud de compu es mayor a 0  hay datos
            fake_db.remove(monit)

        return{
            "message": "success"
        }
    except Exception as e:
        return e