##para cargar primero, descomentar la linea 2 y comentar todo lo demas excepto la  linea 2 y 7
#from fastapi import FastAPI

from fastapi import FastAPI #Se crea el servidor de FastAPI
from routes.computadoras import routes_computadoras#Se importa la clase computadoras de la carpeta routes
from routes.escritorios import routes_escritorios #Se importa la clase computadoras de la carpeta routes

app = FastAPI() #Se crea la variable para tener adentro el servidor FastAPI

app.include_router(routes_computadoras, prefix="/Proyecto_redis") #Se crea la vista

app.include_router(routes_escritorios, prefix="/Proyecto_redis/compu") #Se crea la vista