from fastapi import FastAPI
from sentence_transformers import SentenceTransformer, util
import psycopg2
import os
from dotenv import load_dotenv

load_dotenv()  # Завантажує змінні з .env

app = FastAPI()
model = SentenceTransformer('all-MiniLM-L6-v2')

# Підключення до БД
conn = psycopg2.connect(
    dbname=os.getenv("DB_NAME"),
    user=os.getenv("DB_USER"),
    password=os.getenv("DB_PASSWORD"),
    host=os.getenv("DB_HOST"),
    port=os.getenv("DB_PORT")
)

def load_books():
    with conn.cursor() as cur:
        cur.execute('SELECT book_id, description FROM "Book"')
        rows = cur.fetchall()
        book_ids, descriptions = zip(*rows)
        embeddings = model.encode(descriptions, convert_to_tensor=True)
        return book_ids, descriptions, embeddings

book_ids, descriptions, embeddings = load_books()

@app.get("/similar/{book_id}")
def get_similar_books(book_id: int):
    try:
        index = book_ids.index(book_id)
    except ValueError:
        return {"error": "Book not found"}

    query_embedding = embeddings[index]
    cos_scores = util.pytorch_cos_sim(query_embedding, embeddings)[0]
    top_results = cos_scores.argsort(descending=True)[1:5]  # 3 найсхожі (без себе)

    result = [{"book_id": int(book_ids[idx]), "score": float(cos_scores[idx])} for idx in top_results]
    return result

print("DB_NAME:", os.getenv("DB_NAME"))