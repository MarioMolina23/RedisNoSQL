from fastapi import APIRouter
from redis_client.crud import delete_hash, get_hash, save_hash #Se importa en el FastAPi la clase llamada router
from schemas.computadoras import computadoras #importa routes computadoras


routes_computadoras = APIRouter() #Se crea variable ApiRouter

#guardamos un ejemplo para poder filtrarlo en buscar por ID
#pretende ser la DB
fake_db = [{
  "id": "d634c620-027c-41ff-ab4b-5e3d59184e42",
  "modelo": "ThinkPad",
  "marca": "Lenovo",
  "precio": 800000,
  "almacenamiento": "1TB",
  "color": "Negro",
  "date": "2022-12-03 15:16:10.868199"
}] 


@routes_computadoras.post("/create", response_model=computadoras) #Ruta create tipo POST/agregar
def create(compu: computadoras): #variable es compu de tipo computadoras
    try:
        #operacion DB 
        fake_db.append(compu.dict())

        #operacion cache para guardar en Redis, convertimos los datos a diccionarios con dict()
        save_hash(key=compu.dict()["id"], data=compu.dict())

        return compu
    except Exception as e:
        return e

@routes_computadoras.get("/get/{id}")
def get(id: str):
    try:
        #operacion cache
        data = get_hash(key=id)
        if len(data) == 0: #si es igual a 0 es porque no hay nada en la DB
            #operacion DB
            compu = list(filter(lambda field: field["id"] == id, fake_db))[0]#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID en el indice 0 de la lista
            
            #operacion cache
            save_hash(key=id, data=compu)

            return compu# si no existe en redis devolvemos de memoria
        return data #si existe en redis devolvemos data
    except Exception as e:
        return e

@routes_computadoras.delete("/delete/{id}")
def get(id: str):
    try:
        keyz = computadoras.__fields__.keys() #esto devuelve un array con c/u de las claves
        #operacion cache
        delete_hash(key=id, keys=keyz)

        #operacion DB
        compu = list(filter(lambda field: field["id"] != id, fake_db))#lambda es un FOR pero en una linea, le pasamos el ID para que devuelva solo lo que coincida con ese ID 
        if len(compu) != 0: #si la longitud de compu es mayor a 0  hay datos
            fake_db.remove(compu)

        return{
            "message": "success"
        }
    except Exception as e:
        return e