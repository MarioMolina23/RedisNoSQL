from fastapi import APIRouter
from redis_client.crud import delete_hashescri, get_hashescri, get_hashAllescri, save_hashescri #Se importa en el FastAPi la clase llamada router
from schemas.escritorios import escritorios #importa routes computadoras


routes_escritorios = APIRouter() #Se crea variable ApiRouter

#guardamos un ejemplo para poder filtrarlo en buscar por ID
#pretende ser la DB
fake_db = [] 


@routes_escritorios.post("/create", response_model=escritorios) #Ruta create tipo POST/agregar
def create(escrit: escritorios): #variable es compu de tipo computadoras
    try:
        #operacion DB 
        fake_db.append(escrit.dict())

        #operacion cache para guardar en Redis, convertimos los datos a diccionarios con dict()
        save_hashescri(key=escrit.dict()["id"], data=escrit.dict())

        return escrit
    except Exception as e:
        return e

@routes_escritorios.get("/get/{id}")
def get(id: str):
    try:
        #operacion cache
        data = get_hashescri(key=id)
        if len(data) == 0: #si es igual a 0 es porque no hay nada en la DB
            #operacion DB
            escrit = list(filter(lambda field: field["id"] == id, fake_db))[0]#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID en el indice 0 de la lista
            
            #operacion cache
            save_hashescri(key=id, data=escrit)

            return escrit# si no existe en redis devolvemos de memoria
        return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_escritorios.get("/getAll")
def get():
    try:
        #operacion cache
        data = get_hashAllescri()
        if len(data) != 0: #si es diferente a 0 es porque hay data en la DB
            return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_escritorios.delete("/delete/{id}")
def get(id: str):
    try:
        keyz = escritorios.__fields__.keys() #esto devuelve un array con c/u de las claves
        #operacion cache
        delete_hashescri(key=id, keys=keyz)

        #operacion DB
        escrit = list(filter(lambda field: field["id"] != id, fake_db))#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID 
        if len(escrit) != 0: #si la longitud de compu es mayor a 0  hay datos
            fake_db.remove(escrit)

        return{
            "message": "success"
        }
    except Exception as e:
        return e