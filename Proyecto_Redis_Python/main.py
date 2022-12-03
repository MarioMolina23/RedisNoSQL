from fastapi import FastAPI #Se crea el servidor de FastAPI
from routes.computadoras import routes_computadoras #Se importa la clase computadoras de la carpeta routes

app = FastAPI() #Se crea la variable para tener adentro el servidor FastAPI

app.include_router(routes_computadoras, prefix="/Proyecto_redis") #Se crea la vista


##VOY POR EL MINUTO 3:50 DEL TUTORIAL