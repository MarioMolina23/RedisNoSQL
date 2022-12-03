from fastapi import APIRouter #Se importa en el FastAPi la clase llamada router


routes_computadoras = APIRouter() #Se crea variable ApiRouter

@routes_computadoras.get("/Producto") #Ruta de ejemplo tipo "GET" ---->Esto se depura a lo largo del video
def computadoras():
    return "example"