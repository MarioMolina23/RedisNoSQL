##para cargar primero, descomentar la linea 2 y comentar todo lo demas excepto la  linea 2 y 7
#from fastapi import FastAPI

from fastapi import FastAPI #Se crea el servidor de FastAPI
from routes.computadoras import routes_computadoras#Se importa la clase computadoras de la carpeta routes
from routes.escritorios import routes_escritorios #Se importa la clase computadoras de la carpeta routes
from routes.celulares import routes_celulares #Se importa la clase computadoras de la carpeta routes
from routes.audifonos import routes_audifonos #Se importa la clase computadoras de la carpeta routes
from routes.parlantes import routes_parlantes #Se importa la clase computadoras de la carpeta routes
from routes.estuches import routes_estuches #Se importa la clase computadoras de la carpeta routes
from routes.sillas_gamers import routes_sillas_gamers #Se importa la clase computadoras de la carpeta routes
from routes.monitores import routes_monitores #Se importa la clase computadoras de la carpeta routes

app = FastAPI() #Se crea la variable para tener adentro el servidor FastAPI

app.include_router(routes_computadoras, prefix="/Computadora") #Se crea la vista

app.include_router(routes_escritorios, prefix="/Escritorio") #Se crea la vista

app.include_router(routes_celulares, prefix="/Celulares") #Se crea la 

app.include_router(routes_audifonos, prefix="/Audífonos") #Se crea la 

app.include_router(routes_parlantes, prefix="/Parlantes") #Se crea la 

app.include_router(routes_estuches, prefix="/Estuches") #Se crea la 

app.include_router(routes_sillas_gamers, prefix="/Sillas_gamers") #Se crea la 

app.include_router(routes_monitores, prefix="/Monitores") #Se crea la 