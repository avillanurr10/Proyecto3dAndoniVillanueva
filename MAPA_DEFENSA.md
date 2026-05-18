# 🛡️ MAPA DEFINITIVO Y EXHAUSTIVO DE DEFENSA: PROLEAGUE
**"El documento para el 10 absoluto."** 
Esta guía contiene **absolutamente todas** las funcionalidades del proyecto, con los nombres exactos de los archivos, las funciones clave y la justificación técnica que debes darle al tribunal. Llévala en pantalla o impresa. Cuando te pregunten "Enséñame cómo haces X", busca en este índice, ve a ese archivo en VS Code y brilla.

---

## 🏗️ 1. ARQUITECTURA Y BASES DE DATOS (MySQL + Firestore)
*Pregunta trampa: "¿Por qué has usado dos bases de datos en lugar de una?"*
*   **Respuesta:** *"Usé MySQL para el núcleo de seguridad (usuarios y contraseñas relacionales) y Firestore como base de datos en tiempo real para las interacciones sociales, ya que los WebSockets nativos de Firebase me evitan sobrecargar mi backend con polling."*
*   **MySQL (Datos Crudos):** `backend/db.js` y `backend/controllers/auth.controller.js`. Función clave: `bcrypt.hash(password, 10)`.
*   **Firestore (Social):** `frontend/js/config/firebase-config.js` (Inicialización).

---

## 🔒 2. AUTENTICACIÓN, SEGURIDAD Y PERFIL (El Core)
*   **Login Híbrido (Email o Username):**
    *   **Archivo:** `frontend/js/auth/login.js`. Lógica: Verificas si el input no lleva `@`. Si es Username, haces una petición REST a Firestore para obtener el email asociado, antes de pasárselo al backend.
*   **Session Guard (Reactividad de Sesión Única):**
    *   **Archivo:** `frontend/js/auth/session-guard.js`. Función clave: `onSnapshot(doc(db, "users", uid))`. Detecta si el UUID cambia desde otra pestaña y expulsa al usuario.
*   **Perfil Privado y Avatares (Fallback Dinámico):**
    *   **Archivo:** `frontend/js/user/profile.js`. Función clave: `mostrarDatosPerfil`. Usa el fallback `https://ui-avatars.com/api/?name=${userData.username}&background=random` si no hay foto.
*   **Seguridad en el Perfil (Re-Autenticación):**
    *   **Archivo:** `frontend/js/user/profile.js`. Función clave: `reauthenticateWithCredential`. Obliga a meter la contraseña antigua antes de poder cambiar nombre o contraseña.

---

## 🚀 3. ABSTRACCIÓN DE APIS Y CACHÉ (El Middleware)
*Pregunta trampa: "Enséñame qué pasa si de repente hacen 500 peticiones a los partidos, ¿se cae la API?"*
*   **El Proxy y el `apiCache`:**
    *   **Archivo:** `backend/server.js` (y `services/nbaService.js`). Variable clave: `const apiCache = new Map();`
    *   **Explicación:** *"Mi frontend llama a mi Node.js (`/api/nba/...`). Mi backend comprueba si los datos están en su RAM (`apiCache.get()`). Si están, los devuelve en <150ms. Si no, los pide a la API, los guarda (`apiCache.set()`), y los devuelve. Así evito los errores 429 de BallDontLie y ESPN."*

---

## 📰 4. EL DASHBOARD (Home): RELOJ, PARTIDOS Y NOTICIAS
*   **El Reloj del Tiempo (ET Time):**
    *   **Archivo:** `frontend/js/home/app-home.js`.
    *   **Función clave:** `updateClock()`. Usa `Intl.DateTimeFormat` con `timeZone: "America/New_York"` para que la hora siempre sea la de la costa este (hora de los partidos) independientemente de dónde esté el usuario.
*   **Scoreboard (Los Partidos Recientes):**
    *   **Archivo:** `frontend/js/home/app-home.js`.
    *   **Función clave:** `cargarScoreboard()`. Hace un `Promise.all` al backend para traérse los resultados de NBA y NFL, los ordena por fecha y pinta un carrusel dinámico en el `games-ticker`.
*   **Noticias RSS y Parseo XML (Data):**
    *   **Archivos:** `backend/controllers/news.controller.js`. Función clave: `cheerio.load(xmlData)`.
    *   **Explicación:** *"ESPN da un feed XML antiguo. Uso la librería `cheerio` para leerlo como si fuera un DOM HTML, extraer `<title>` y `<link>`, y enviarlo como JSON limpio al frontend."*
*   **Comentarios en Tiempo Real en las Noticias:**
    *   **Archivo:** `frontend/js/home/app-home.js`.
    *   **Función clave:** `setupNewsInteractions`. Usa `onSnapshot(doc(db, "news_interactions", newsId))` de Firebase para que si un usuario comenta una noticia, a los demás les aparezca el comentario en tiempo real sin recargar la página.

---

## 🏀 5. LIGAS (NBA / NFL) Y BUSCADOR DE JUGADORES
*   **Renderizado de Ligas:**
    *   **Archivos:** `frontend/js/leagues/app-nba.js` y `app-nfl.js`.
    *   **Qué enseñar:** Los `fetch()` hacia `/api/nba/standings` inyectando filas de tablas dinámicamente con `innerHTML`.
*   **Buscador Inteligente de Jugadores:**
    *   **Archivo:** `frontend/js/players/app-players.js`.
    *   **Función clave:** `fetchPlayers()`.
    *   **Explicación:** Implementa `searchCache = new Map()` en el lado del cliente (Front) para no hacer peticiones si ya buscaste a "LeBron" hace 10 segundos, optimizando aún más el consumo.

---

## 👥 6. COMUNIDAD, BÚSQUEDA Y CHAT
*   **Buscador de Usuarios Limpio:**
    *   **Archivo:** `frontend/js/user/search-users.js`.
    *   **Qué enseñar:** Explica cómo te traes los documentos y usas el campo `cleanUsername` para evitar duplicados en los resultados.
*   **Chat Bidireccional Seguro (WebSockets):**
    *   **Archivos:** `frontend/js/chat/chat.js` (Cliente) y `backend/server.js` (Servidor).
    *   **Explicación:** *"Uso Socket.io en vez de Firestore porque Firestore cobra por lecturas. Socket.io maneja miles de mensajes en la RAM de mi servidor Node.js gratis."*
    *   **Seguridad XSS:** Muestra en `chat.js` cómo limpias los mensajes de entrada escapando caracteres `<` para evitar inyecciones de código.
*   **Perfil Público de Terceros:**
    *   **Archivo:** `frontend/js/user/public-profile.js`. Solo devuelves datos estéticos (username, avatar, dream team), nunca credenciales.

---

## 🛠️ 7. HERRAMIENTAS PREMIUM Y FAVORITOS
*   **Dream Team Builder:**
    *   **Archivo:** `frontend/js/user/dreamteam.js`.
    *   **Función clave:** Validar las posiciones con el diccionario estático `const CONFIG = { NBA: { positions: ["PG", "SG", "SF", "PF", "C"] } }`. Si la posición que devuelve la API no coincide, el sistema rechaza meter al jugador.
*   **Gráficos Analíticos (Comparativa):**
    *   **Archivo:** `frontend/js/analytics/comparison.js` (y `trending.js`).
    *   **Qué enseñar:** La integración de `Chart.js` (`new Chart(...)`). Cómo extraes PTS, REB y AST y generas un polígono radar interactivo.
*   **Sistema de Favoritos (Equipos):**
    *   **Archivo:** `frontend/js/user/app-favorites.js` y `profile.js` (`renderFavoritos`).
    *   **Lógica en el Header:** En `app-home.js`, la función `updateFavBadge()` cuenta los favoritos guardados en localStorage y muestra una burbujita (badge) de notificación dinámica en el Header.

---

## 💎 8. UI/UX Y ESTANDARIZACIÓN CORPORATIVA
*   **La Gestión del Header Global:**
    *   **Archivo:** `frontend/js/utils/header-logic.js`. 
    *   **Qué enseñar:** Centralizaste la lógica del menú hamburguesa, la inserción del nombre del usuario y el botón de logout para no repetir código en cada página HTML.
*   **Coach Pro AI (El Bot / Mascota Virtual):**
    *   **Archivo:** `frontend/js/utils/coach-pro.js`.
    *   **Explicación:** Una mascota animada con CSS interactiva. Muestra la función que usa `Math.random()` para escupir consejos deportivos ("tips") aleatoriamente en burbujas de diálogo dinámicas.
*   **Material Icons vs Emojis:**
    *   **Archivos:** `index.html` (Cabecera).
    *   **Explicación:** *"Los emojis nativos (✅, 🏀) se ven distintos en iOS y Windows. Los sustituí por `<span class="material-icons">`, garantizando píxel-perfect en cualquier dispositivo."*
*   **Deuda Técnica del CSS (La excusa perfecta):**
    *   **Archivo:** `frontend/css/style.css`.
    *   **Aprende esto de memoria:** *"El 'Dark Glassmorphism' requiere propiedades muy pesadas: `backdrop-filter: blur`, variables nativas, animaciones `skeletons`. El archivo llegó a +6000 líneas. Mi primera tarea de mejora futura sería migrar a SCSS (SASS). Prioricé la estabilidad funcional antes que refactorizar todo el diseño a última hora."*
*   **Mapeo Estático de Logos:**
    *   **Archivo:** `frontend/js/config/logos-config.js`.
    *   **Explicación:** *"ESPN llama a un equipo 'L.A. Lakers' y BallDontLie 'Los Angeles Lakers'. Este diccionario hace de traductor para que el escudo nunca falle."*

---

## 🛑 CONSEJOS CLAVE PARA EL TRIBUNAL
1.  **NO uses el ratón para abrir archivos.** Pulsa **`Ctrl + P`**, teclea `app-home.js` o el que quieras, y pulsa Enter. Demuestra dominio Senior.
2.  **Busca líneas rápido:** Pulsa **`Ctrl + F`** dentro del archivo (ej. buscas `apiCache`, `socket.io` o `onSnapshot`).
3.  **Llévalos a tu terreno:** Si dudan de algo visual, enséñales el bot `coach-pro.js`. Si dudan de rendimiento, salta a `server.js` y enséñales la caché.
4.  **Si te dicen "Falta X":** *"Totalmente cierto. Fue una decisión de arquitectura. Por límites del MVP lo tengo apuntado en la memoria (Líneas Futuras) para una próxima iteración."*
