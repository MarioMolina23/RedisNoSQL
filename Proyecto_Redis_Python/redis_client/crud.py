from .connection import redis_client
from redis.exceptions import ResponseError

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