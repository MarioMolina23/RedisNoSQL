from .connection import redis_client
from redis.exceptions import ResponseError

#--------------------COMPUTADORA---------------------------#
def save_hash(key: str, data: dict):
    try:
        redis_client.hset(name=key, mapping=data)
    except ResponseError as e:
        print(e)

def get_hash(key: str):
    try:
        return redis_client.hgetall(name=key)
    except ResponseError as e:
        print(e)

def get_hashAll():
    try:
        datos = []
        keys = redis_client.keys(pattern="computadoras*")
        for key in keys:
            value = redis_client.hgetall(name=key)
            datos.append(value)
        
        return datos
    except ResponseError as e:
        print(e)

def delete_hash(key: str, keys: list):
    
    #keys son los que queremos eliminar
    try:
        redis_client.hdel(key, *keys)
    except ResponseError as e:
        print(e)

#--------------------ESCRITORIO---------------------------#

def save_hashescri(key: str, data: dict):
    try:
        redis_client.hset(name=key, mapping=data)
    except ResponseError as e:
        print(e)

def get_hashescri(key: str):
    try:
        return redis_client.hgetall(name=key)
    except ResponseError as e:
        print(e)

def get_hashAllescri():
    try:
        datos = []
        keys = redis_client.keys(pattern="escritorios*")
        for key in keys:
            value = redis_client.hgetall(name=key)
            datos.append(value)
        
        return datos
    except ResponseError as e:
        print(e)

def delete_hashescri(key: str, keys: list):
    
    #keys son los que queremos eliminar
    try:
        redis_client.hdel(key, *keys)
    except ResponseError as e:
        print(e)


#--------------------AUDIFONOS---------------------------#

def save_hashaudif(key: str, data: dict):
    try:
        redis_client.hset(name=key, mapping=data)
    except ResponseError as e:
        print(e)

def get_hashaudif(key: str):
    try:
        return redis_client.hgetall(name=key)
    except ResponseError as e:
        print(e)

def get_hashAllaudif():
    try:
        datos = []
        keys = redis_client.keys(pattern="audifonos*")
        for key in keys:
            value = redis_client.hgetall(name=key)
            datos.append(value)
        
        return datos
    except ResponseError as e:
        print(e)

def delete_hashaudif(key: str, keys: list):
    
    #keys son los que queremos eliminar
    try:
        redis_client.hdel(key, *keys)
    except ResponseError as e:
        print(e)

#--------------------CELULARES---------------------------#

def save_hashcelul(key: str, data: dict):
    try:
        redis_client.hset(name=key, mapping=data)
    except ResponseError as e:
        print(e)

def get_hashcelul(key: str):
    try:
        return redis_client.hgetall(name=key)
    except ResponseError as e:
        print(e)

def get_hashAllcelul():
    try:
        datos = []
        keys = redis_client.keys(pattern="celulares*")
        for key in keys:
            value = redis_client.hgetall(name=key)
            datos.append(value)
        
        return datos
    except ResponseError as e:
        print(e)

def delete_hashcelul(key: str, keys: list):
    
    #keys son los que queremos eliminar
    try:
        redis_client.hdel(key, *keys)
    except ResponseError as e:
        print(e)

#--------------------ESTUCHES---------------------------#

def save_hashestuc(key: str, data: dict):
    try:
        redis_client.hset(name=key, mapping=data)
    except ResponseError as e:
        print(e)

def get_hashestuc(key: str):
    try:
        return redis_client.hgetall(name=key)
    except ResponseError as e:
        print(e)

def get_hashAllestuc():
    try:
        datos = []
        keys = redis_client.keys(pattern="estuches*")
        for key in keys:
            value = redis_client.hgetall(name=key)
            datos.append(value)
        
        return datos
    except ResponseError as e:
        print(e)

def delete_hashestuc(key: str, keys: list):
    
    #keys son los que queremos eliminar
    try:
        redis_client.hdel(key, *keys)
    except ResponseError as e:
        print(e)        

#--------------------MONITORES---------------------------#

def save_hashmonit(key: str, data: dict):
    try:
        redis_client.hset(name=key, mapping=data)
    except ResponseError as e:
        print(e)

def get_hashmonit(key: str):
    try:
        return redis_client.hgetall(name=key)
    except ResponseError as e:
        print(e)

def get_hashAllmonit():
    try:
        datos = []
        keys = redis_client.keys(pattern="monitores*")
        for key in keys:
            value = redis_client.hgetall(name=key)
            datos.append(value)
        
        return datos
    except ResponseError as e:
        print(e)

def delete_hashmonit(key: str, keys: list):
    
    #keys son los que queremos eliminar
    try:
        redis_client.hdel(key, *keys)
    except ResponseError as e:
        print(e)        

#--------------------PARLANTES---------------------------#

def save_hashparla(key: str, data: dict):
    try:
        redis_client.hset(name=key, mapping=data)
    except ResponseError as e:
        print(e)

def get_hashparla(key: str):
    try:
        return redis_client.hgetall(name=key)
    except ResponseError as e:
        print(e)

def get_hashAllparla():
    try:
        datos = []
        keys = redis_client.keys(pattern="parlantes*")
        for key in keys:
            value = redis_client.hgetall(name=key)
            datos.append(value)
        
        return datos
    except ResponseError as e:
        print(e)

def delete_hashparla(key: str, keys: list):
    
    #keys son los que queremos eliminar
    try:
        redis_client.hdel(key, *keys)
    except ResponseError as e:
        print(e)

#--------------------SILLAS GAMERS---------------------------#

def save_hashsillag(key: str, data: dict):
    try:
        redis_client.hset(name=key, mapping=data)
    except ResponseError as e:
        print(e)

def get_hashsillag(key: str):
    try:
        return redis_client.hgetall(name=key)
    except ResponseError as e:
        print(e)

def get_hashAllsillag():
    try:
        datos = []
        keys = redis_client.keys(pattern="sillas_gamers*")
        for key in keys:
            value = redis_client.hgetall(name=key)
            datos.append(value)
        
        return datos
    except ResponseError as e:
        print(e)

def delete_hashsillag(key: str, keys: list):
    
    #keys son los que queremos eliminar
    try:
        redis_client.hdel(key, *keys)
    except ResponseError as e:
        print(e)        