# 🎯 GUÍA PRÁCTICA: FILTRADO AVANZADO CON EJEMPLOS

## ¿Qué Cambió?

Antes: Solo podías filtrar por **Búsqueda** y **Pet Friendly**
Ahora: Puedes filtrar por **8+ criterios avanzados** simultáneamente

---

## 📊 Los 8+ Filtros Explicados

### 1️⃣ Búsqueda General (ANTES Y AHORA)
```
¿Qué hace? Busca palabras en nombre, descripción, tipo
Ejemplo: "luxury" → busca "Luxury Villa with Pool"
```

### 2️⃣ Pet Friendly (ANTES Y AHORA)
```
¿Qué hace? Detecta "pets", "mascotas", "allowed", "permitido"
Ejemplo: ✓ Muestra solo propiedades que permiten mascotas
```

### 3️⃣ 🆕 Tipo de Propiedad
```
¿Qué hace? Filtra por tipo de alojamiento
Opciones: ComboBox con tipos encontrados en el JSON
Ejemplo: Selecciona "Villa" → solo muestra villas
```

### 4️⃣ 🆕 Precio Mínimo
```
¿Qué hace? Muestra propiedades con precio ≥ valor
Rango: $0 - $100,000
Ejemplo: "100" → solo propiedades de $100 o más
```

### 5️⃣ 🆕 Precio Máximo
```
¿Qué hace? Muestra propiedades con precio ≤ valor
Rango: $0 - $100,000
Ejemplo: "200" → solo propiedades hasta $200
```

### 6️⃣ 🆕 Dormitorios Mínimo
```
¿Qué hace? Muestra propiedades con ≥ dormitorios
Rango: 0 - 20
Ejemplo: "2" → solo propiedades con 2+ dormitorios
```

### 7️⃣ 🆕 Dormitorios Máximo
```
¿Qué hace? Muestra propiedades con ≤ dormitorios
Rango: 0 - 20
Ejemplo: "3" → solo propiedades con hasta 3 dormitorios
```

### 8️⃣ 🆕 Rating Mínimo
```
¿Qué hace? Muestra propiedades con rating ≥ valor
Rango: 0 - 5 (con decimales 0.1)
Ejemplo: "4.5" → solo propiedades con rating 4.5+
```

---

## 🎬 Ejemplos Prácticos Paso a Paso

### 📌 Ejemplo 1: Encontrar Apartamentos Económicos

**Objetivo:** Encontrar apartamentos baratos, pequeños y bien calificados

**Pasos:**
```
1. Carga sample_airbnb.json
2. ComboBox Tipo → Selecciona "Apartment"
3. Precio Mínimo → Deja en 0
4. Precio Máximo → Establece 100
5. Dormitorios Mínimo → Deja en 0
6. Dormitorios Máximo → Establece 2
7. Rating Mínimo → Establece 4.0
8. Pet Friendly → Deja sin marcar
9. Búsqueda → Deja vacía
```

**Resultado esperado:** Apartamentos de hasta $100, máx 2 dormitorios, rating 4.0+

---

### 📌 Ejemplo 2: Viaje Lujo Pet-Friendly

**Objetivo:** Pasar vacaciones con tu perro en una villa lujosa

**Pasos:**
```
1. ComboBox Tipo → "Villa"
2. Precio Mínimo → 150
3. Precio Máximo → 1000 (sin límite superior)
4. Dormitorios Mínimo → 3
5. Dormitorios Máximo → 20 (sin límite)
6. Rating Mínimo → 4.7
7. Pet Friendly → ✓ MARCA ESTO
8. Búsqueda → "luxury" (opcional)
```

**Resultado esperado:** Villas lujosas pet-friendly de $150+, 3+ dormitorios, rating 4.7+

---

### 📌 Ejemplo 3: Casa Familiar Presupuestada

**Objetivo:** Casa para familia con 2-3 hijos, presupuesto limitado

**Pasos:**
```
1. ComboBox Tipo → "House"
2. Precio Mínimo → 0
3. Precio Máximo → 150
4. Dormitorios Mínimo → 3
5. Dormitorios Máximo → 5
6. Rating Mínimo → 4.0
7. Pet Friendly → ✗ (sin marcar - mascotas no importan)
8. Búsqueda → "family" o "spacious"
```

**Resultado esperado:** Casas de $0-150 con 3-5 dormitorios, rating 4.0+

---

### 📌 Ejemplo 4: Búsqueda Muy Específica

**Objetivo:** Encontrar la propiedad perfecta con criterios múltiples

**Pasos:**
```
1. Búsqueda → Escribe "downtown"
2. ComboBox Tipo → "Loft"
3. Precio Mínimo → 100
4. Precio Máximo → 180
5. Dormitorios Mínimo → 2
6. Dormitorios Máximo → 2
7. Rating Mínimo → 4.5
8. Pet Friendly → ✓ MARCA ESTO
```

**Resultado esperado:** Lofts en downtown, $100-180, exactamente 2 dormitorios, rating 4.5+, pet-friendly

---

### 📌 Ejemplo 5: Viaje de Mochilero (Budget)

**Objetivo:** Lo más barato posible, bien calificado

**Pasos:**
```
1. Precio Máximo → 75 (muy económico)
2. Dormitorios Mínimo → 1
3. Rating Mínimo → 4.0 (mínimo aceptable)
4. Todo lo demás → Sin límite
```

**Resultado esperado:** Las opciones más baratas pero bien calificadas

---

### 📌 Ejemplo 6: Sin Filtros (Ver Todos)

**Objetivo:** Ver todas las propiedades disponibles

**Pasos:**
```
1. Click en "🔄 Limpiar Filtros"
   O
1. Dejar todos los campos con valores por defecto:
   - Búsqueda: vacía
   - Pet Friendly: desmarcado
   - Tipo: "(Todos los tipos)"
   - Precio: 0 - 100000
   - Dormitorios: 0 - 20
   - Rating: 0
```

**Resultado esperado:** Todos los 5 registros de sample_airbnb.json

---

## 🔄 Validaciones Automáticas

### Validación 1: Precio Mínimo > Máximo
```
Si estableces:
- Mín: 200
- Máx: 100

El sistema automáticamente:
- Ajusta Máx a 200
- Advierte visualmente
```

### Validación 2: Dormitorios Mín > Máx
```
Si estableces:
- Mín: 4
- Máx: 2

El sistema automáticamente:
- Ajusta Máx a 4
```

---

## 📊 Label de Filtros Activos

### Ejemplo 1: Sin filtros
```
"Filtros activos: ninguno"
```

### Ejemplo 2: Con filtros
```
"Filtros activos: Búsqueda: "villa" | Tipo: Villa | Precio mín: $150 | Rating mín: 4.5 | Pet Friendly: Sí"
```

### Ejemplo 3: Solo rango de precio
```
"Filtros activos: Precio mín: $100 | Precio máx: $200"
```

---

## 🔍 Columnas que Busca Cada Filtro

### Para Tipo de Propiedad
```
Columna: "property_type"
Ejemplo: "Apartment", "Villa", "House", "Studio"
```

### Para Precio
```
Columnas (en orden de búsqueda):
1. price_per_night (prioritaria)
2. price
3. cost
4. nightly_rate
5. rate

Ejemplo valor: 89, 150.50, 250
```

### Para Dormitorios
```
Columnas (en orden):
1. bedrooms (prioritaria)
2. rooms
3. bed_rooms
4. num_bedrooms

Ejemplo valor: 1, 2, 3, 4
```

### Para Rating
```
Columnas (en orden):
1. rating (prioritaria)
2. review_rating
3. score
4. stars
5. average_rating

Ejemplo valor: 4.8, 4.5, 3.9
```

---

## 💡 Tips y Trucos

### Tip 1: Combinar Búsqueda + Filtros
```
Búsqueda: "downtown"
+ Tipo: "Apartment"
+ Precio: $100-150
= Solo apartamentos en downtown de $100-150
```

### Tip 2: Ver Rango de Precios
```
Establece Precio Mín: 0
Establece Precio Máx a valores diferentes para explorar:
- $0-100: Opciones económicas
- $100-200: Rango medio
- $200+: Opciones premium
```

### Tip 3: Encontrar Joyitas (Alto Rating + Precio Bajo)
```
Precio Máx: 100
Rating Mín: 4.6
= Propiedades baratas pero excelentes
```

### Tip 4: Explorar por Tipo
```
1. Selecciona cada tipo uno a uno
2. Observa características de cada tipo
3. Encuentra el que más te guste
```

### Tip 5: Usar Búsqueda para Detalles
```
Búsqueda: "swimming" → Propiedades con piscina
Búsqueda: "garden" → Propiedades con jardín
Búsqueda: "parking" → Propiedades con estacionamiento
```

---

## 🎨 Escenarios Reales

### Escenario 1: Reservar para Fin de Semana
```
Presupuesto: $100-150
Tipo: Flexible
Dormitorios: 1-2
Rating: 4.5+
Pet Friendly: Sí (tengo un perro)
Búsqueda: "cozy" o "downtown"
```

### Escenario 2: Reunión de Negocios
```
Presupuesto: $200-500 (empresarial)
Tipo: "Loft" preferentemente
Dormitorios: 2+
Rating: 4.7+
Búsqueda: "modern" o "business district"
```

### Escenario 3: Vacaciones en Familia
```
Presupuesto: $150-300
Tipo: "House" o "Villa"
Dormitorios: 3-4 (para 6 personas)
Rating: 4.6+
Pet Friendly: No importa
Búsqueda: "family" o "spacious"
```

### Escenario 4: Viaje Económico Solo
```
Presupuesto: $50-80 (muy económico)
Tipo: "Studio" o "Apartment"
Dormitorios: 1
Rating: 4.0+ (aceptable)
Pet Friendly: No importa
```

---

## 📈 Cómo Resultan los Filtros

### Todos Activos (AND Logic)
```
Si estableces:
✓ Búsqueda: "villa"
✓ Tipo: "Villa"
✓ Precio: $150-300
✓ Dormitorios: 3+
✓ Rating: 4.6+
✓ Pet Friendly: Sí

Resultado: Villas GRANDES con villa en el nombre
            Y precio $150-300
            Y 3+ dormitorios
            Y rating 4.6+
            Y pet-friendly
```

### Parcialmente Activos
```
Si estableces:
✓ Precio: $100-200
✓ Rating: 4.5+
✗ Todo lo demás vacío

Resultado: Cualquier propiedad de $100-200
           Y rating 4.5+
           (Tipo, dormitorios, búsqueda no importan)
```

---

## 🧪 Pruebas Sugeridas

### Test 1: Combinación Extreme
```
Precio: $1-50 (muy barato)
Dormitorios: 5+ (muy grande)
Rating: 4.9+ (excelente)
Resultado: Probablemente ninguno (conflictivo)
```

### Test 2: Parámetros Amplios
```
Precio: $0-100000 (sin límite)
Dormitorios: 0-20 (sin límite)
Rating: 0 (sin límite)
Resultado: Todos los registros
```

### Test 3: Solo Precio
```
Precio: $90-120
(Todo lo demás por defecto)
Resultado: Solo registros en ese rango
```

### Test 4: Búsqueda + Tipo
```
Búsqueda: "apartment"
Tipo: "Apartment"
(Sin límites en precio/dormitorios/rating)
Resultado: Apartamentos con "apartment" en descripción
```

---

## ❌ Errores Comunes (y Soluciones)

### Error 1: No Aparece Nada
```
Causa posible: Filtros demasiado restrictivos
Solución: Click en "🔄 Limpiar Filtros" y vuelve a intentar
```

### Error 2: Aparecen Menos de lo Esperado
```
Causa: Los filtros se aplican con AND (todos se cumplen)
Solución: Deja algunos filtros sin límite (valores por defecto)
```

### Error 3: No Funciona el Filtro de Tipo
```
Causa: Puede que el JSON no tenga columna "property_type"
Solución: Verifica que tu JSON tenga esa columna
```

### Error 4: Cambios No Se Aplican
```
Causa: Puede necesitar compilar/reiniciar
Solución: Presiona F5 para compilar y ejecutar
```

---

## 🎯 Resumen Rápido

**Antes:** 2 filtros  
**Ahora:** 8+ filtros avanzados  
**Interfaz:** Mejorada y clara  
**Validaciones:** Automáticas  
**Performance:** Tiempo real (sin lag)  
**Compatibilidad:** Backward compatible  

---

**¡Ya estás listo para explorar tus datos con filtrado profesional!** 🚀
