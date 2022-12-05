from pydantic import BaseModel, Field
from uuid import uuid4 #para generar ID automaticamente
from datetime import datetime

#genera ID automatico
def generate_uuid():
    return str(f'audifonos{uuid4()}')

#genera fecha
def generate_date():
    return str(datetime.now())

class audifonos(BaseModel): #clase que recibe BaseModel
    id: str = Field(default_factory=generate_uuid) #llama autogenera ID
    modelo: str
    marca: str
    precio: float
    almacenamiento: str
    color: str
    date: str = Field(default_factory=generate_date) #llama autogenera fecha