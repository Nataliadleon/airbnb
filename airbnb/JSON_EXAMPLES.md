# Ejemplos de Archivos JSON para Probar

Este documento contiene varios ejemplos de JSON que puedes copiar en un archivo `.json` para probar la aplicación y verificar su adaptabilidad.

## 📝 Ejemplo 1: Airbnb Completo (RECOMENDADO)

**Archivo:** `sample_airbnb.json` (ya incluido)

Este es el archivo que viene con la aplicación. Contiene:
- 5 propiedades variadas
- Mezcla de amenities (array)
- House rules con palabras clave de mascotas
- Precios, ratings y ubicaciones

```json
[
  {
    "id": 1,
    "name": "Cozy Apartment in Downtown",
    "description": "A beautiful apartment in the heart of the city, perfect for travelers",
    "property_type": "Apartment",
    "price_per_night": 89,
    "amenities": ["WiFi", "Pets allowed", "Air conditioning", "Kitchen"],
    "house_rules": "No smoking, pets welcome",
    "bedrooms": 2,
    "bathrooms": 1,
    "rating": 4.8
  }
]
```

---

## 🏢 Ejemplo 2: Estructura Mínima

Para probar adaptabilidad a JSONs simples:

```json
[
  {
    "name": "Simple Apartment",
    "price": 75
  },
  {
    "name": "Simple House",
    "price": 150
  }
]
```

---

## 🌍 Ejemplo 3: Airbnb Internacional (Multi-idioma)

```json
[
  {
    "id": 1001,
    "titulo": "Apartamento en Madrid",
    "descripcion": "Hermoso apartamento permitido mascotas en el centro",
    "tipo": "Apartment",
    "precio": 95,
    "servicios": ["WiFi", "Mascotas permitidas", "Aire acondicionado"],
    "normas": "Se permiten mascotas, no fumar",
    "dormitorios": 2,
    "estrellas": 4.7
  },
  {
    "id": 1002,
    "titulo": "Casa en Barcelona",
    "descripcion": "Villa con piscina y jardín, no se permiten animales",
    "tipo": "Casa",
    "precio": 200,
    "servicios": ["WiFi", "Piscina", "Jardín"],
    "normas": "No se permiten mascotas",
    "dormitorios": 4,
    "estrellas": 4.9
  }
]
```

---

## 🎭 Ejemplo 4: Amenities como Objetos Anidados

La app normaliza automáticamente objetos complejos:

```json
[
  {
    "id": 2001,
    "name": "Luxury Penthouse",
    "amenities": {
      "internet": "Fiber WiFi",
      "kitchen": "Full equipped",
      "pets": "Allowed"
    },
    "house_rules": "Pets welcome with approval",
    "price": 300
  }
]
```

---

## 🏠 Ejemplo 5: Diferentes Tipos de Propiedades

```json
[
  {
    "id": 3001,
    "name": "Beachfront Bungalow",
    "type": "Bungalow",
    "description": "Pet-friendly beach house",
    "amenities": ["Beach access", "Pets allowed", "WiFi"],
    "price": 120
  },
  {
    "id": 3002,
    "name": "Mountain Cabin",
    "type": "Cabin",
    "description": "Cozy cabin, mascotas bienvenidas",
    "amenities": ["Fireplace", "Pets allowed"],
    "price": 85
  },
  {
    "id": 3003,
    "name": "Urban Studio",
    "type": "Studio",
    "description": "Compact modern studio",
    "amenities": ["WiFi", "No pets"],
    "price": 65
  },
  {
    "id": 3004,
    "name": "Country Manor",
    "type": "Manor",
    "description": "Large estate with gardens",
    "amenities": ["Large grounds", "Pets allowed", "WiFi"],
    "price": 400
  }
]
```

---

## 🎯 Ejemplo 6: Prueba de Búsqueda (Nombres Largos)

Para probar filtrado por descripciones extensas:

```json
[
  {
    "name": "Beautiful Downtown Loft with Modern Amenities",
    "description": "This stunning loft features floor-to-ceiling windows with city views, stainless steel appliances, and a private balcony. Pets are absolutely allowed and welcomed here!",
    "location": "Downtown Core",
    "amenities": ["WiFi", "Air conditioning", "Pets allowed"],
    "price": 150
  },
  {
    "name": "Cozy Suburban House",
    "description": "Perfect family home in quiet neighborhood. Sorry, no pets allowed per house association rules.",
    "location": "Suburbs",
    "amenities": ["WiFi", "Large yard"],
    "price": 120
  }
]
```

---

## 🔄 Ejemplo 7: Datos Inconsistentes (Prueba de Robustez)

La app maneja columnas faltantes inteligentemente:

```json
[
  {
    "name": "Complete Property",
    "price": 100,
    "amenities": "Pets allowed",
    "description": "Has all fields",
    "rating": 4.5
  },
  {
    "name": "Minimal Property",
    "price": 80
  },
  {
    "name": "Different Structure",
    "title": "Alternative Title",
    "cost": 120,
    "summary": "Pets welcome"
  }
]
```

---

## 🌟 Ejemplo 8: Objeto Único (No Array)

La app convierte automáticamente:

```json
{
  "id": 5001,
  "name": "Single Property Listing",
  "description": "A single property loaded as JSON object",
  "amenities": ["WiFi", "Pets allowed"],
  "price": 95
}
```

**Resultado:** Se carga como si fuera un array de 1 elemento.

---

## 📊 Ejemplo 9: Datos Grandes con Múltiples Filtros

```json
[
  {
    "id": 1,
    "name": "Pet Paradise Apartment",
    "description": "Modern apartment in trendy neighborhood",
    "property_type": "Apartment",
    "amenities": ["Pets allowed", "WiFi", "A/C"],
    "house_rules": "Pets welcome",
    "price": 110,
    "rating": 4.8
  },
  {
    "id": 2,
    "name": "Luxury No-Pet Villa",
    "description": "Exclusive villa for travelers without animals",
    "property_type": "Villa",
    "amenities": ["Pool", "Gym", "Concierge"],
    "house_rules": "No pets",
    "price": 350,
    "rating": 4.9
  },
  {
    "id": 3,
    "name": "Dog-Friendly Cottage",
    "description": "Perfect for travelers with four-legged friends",
    "property_type": "Cottage",
    "amenities": ["Mascotas permitidas", "Garden", "WiFi"],
    "house_rules": "Dogs and cats allowed",
    "price": 95,
    "rating": 4.6
  },
  {
    "id": 4,
    "name": "Budget Studio",
    "description": "Simple and affordable studio",
    "property_type": "Studio",
    "amenities": ["WiFi"],
    "house_rules": "Standard rules apply",
    "price": 60,
    "rating": 4.3
  },
  {
    "id": 5,
    "name": "Pet Sanctuary House",
    "description": "Entire house dedicated to pet owners",
    "property_type": "House",
    "amenities": ["Large yard", "Pets allowed", "WiFi"],
    "house_rules": "Pets absolutely welcome here",
    "price": 180,
    "rating": 4.7
  }
]
```

---

## 🧪 Cómo Usar Estos Ejemplos

### Opción 1: Copiar directamente
1. Selecciona el JSON que quieres probar
2. Cópialo (Ctrl+C)
3. Abre un editor de texto (Notepad, VS Code)
4. Pega el JSON
5. Guarda como `test_*.json` (ej: `test_minimal.json`)
6. En la app, haz clic en "📁 Cargar JSON" y selecciona tu archivo

### Opción 2: Usar PowerShell
```powershell
# Crear archivo desde contenido
@'
[{"name": "Test", "price": 100}]
'@ | Out-File -FilePath "test_data.json" -Encoding UTF8

# La app puede cargar este archivo
```

---

## ✅ Checklist de Pruebas

Usa estos ejemplos para verificar todas las funcionalidades:

- [ ] **Estructura Mínima** - Prueba adaptabilidad
- [ ] **Airbnb Completo** - Verifica todo funciona
- [ ] **Multi-idioma** - Búsqueda en español
- [ ] **Nested Objects** - Normalización de complejos
- [ ] **Inconsistentes** - Robustez ante campos faltantes
- [ ] **Búsqueda Larga** - Rendimiento con descripciones
- [ ] **Pet Friendly** - Todos los formatos (pets, mascotas, allowed, permitido)
- [ ] **Objeto Único** - Conversión automática a array

---

## 🐛 Problemas Comunes al Crear JSON

### ❌ JSON Inválido
```json
{name: "Property"}  // Error: nombre sin comillas
```

### ✅ JSON Válido
```json
{"name": "Property"}  // Correcto: comillas en nombres
```

### ❌ Sin Comillas en Strings
```json
{"description": Property description}  // Error
```

### ✅ Con Comillas
```json
{"description": "Property description"}  // Correcto
```

### ❌ Comas Faltantes
```json
[
  {"id": 1}
  {"id": 2}  // Error: falta coma entre objetos
]
```

### ✅ Con Comas
```json
[
  {"id": 1},
  {"id": 2}  // Correcto
]
```

---

## 🔗 Herramientas Útiles

Para validar tus JSONs:
- **jsonlint.com** - Validador online
- **jsoncrack.com** - Visualizador de JSON
- **Visual Studio Code** - Validación automática

---

¡Diviértete probando diferentes estructuras JSON! 🚀
