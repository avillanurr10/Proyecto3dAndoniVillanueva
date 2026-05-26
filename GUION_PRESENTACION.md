# 🎙️ GUION DE PRESENTACIÓN: PROLEAGUE (10 MINUTOS EXACTOS)

Este guion está diseñado milimétricamente para **cumplir con el 100% de la rúbrica de presentación**, evitando penalizaciones por tiempo y garantizando las máximas notas en "Presentación General", "Proceso de Desarrollo" y "Demo Funcional".

**Regla de Oro:** **NO LEAS.** Usa este documento para ensayar en casa. En la presentación, tu pantalla debe estar dividida o ir cambiando entre la web funcionando y tu código (VS Code) o una diapositiva de apoyo.

---

## ⏱️ FASE 1: PRESENTACIÓN GENERAL (Minuto 0:00 - 1:30)
*Objetivo Rúbrica (1 punto): Explicar problema, usuarios, objetivos, valor añadido y alcance.*

*(En pantalla: La portada de tu aplicación ProLeague o una diapositiva con el logo)*

**Tú dices:**
> "Buenos días al tribunal. Soy Andoni Villanueva y voy a presentar **ProLeague**, una plataforma web orientada al análisis deportivo y la interacción social de fans de la NBA y la NFL. 
> 
> El problema que detecté es que las apps deportivas actuales son puramente informativas o puramente sociales. Los usuarios tienen que saltar entre ESPN para leer noticias, a X (Twitter) para comentar, y a otras webs para ver estadísticas. 
> 
> Mi objetivo con ProLeague era unificar esto: crear un **ecosistema integral** donde un usuario puede consultar marcadores en vivo, consumir noticias RSS en tiempo real y, a la vez, interactuar con una comunidad formando su propio *Dream Team* y chateando en directo.
>
> A nivel de alcance, el MVP incluye un backend propio en Node.js, una estrategia de base de datos dual (MySQL y Firestore), y un frontend robusto con Vanilla JavaScript."

---

## ⏱️ FASE 2: LA DEMO FUNCIONAL (Minuto 1:30 - 6:30)
*Objetivo Rúbrica (2.5 puntos): Flujos principales muy bien preparados y sin fallos.*

*(En pantalla: Haz exactamente lo que vas diciendo. Ve a un ritmo ágil).*

**[Min 1:30] Autenticación y Seguridad:**
> "Empezando por el acceso, **tanto el frontend como el backend están completamente desplegados en producción**. Para garantizar un flujo seguro, el registro requiere **verificación obligatoria por correo electrónico** antes de permitir el primer login, y la plataforma cuenta con un sistema de **recuperación de cuenta automatizado** mediante Firebase. Además, implementé un **Login Híbrido**, permitiendo entrar con correo o con nombre de usuario. Pero el valor añadido aquí es la seguridad: si inicio sesión... *(inicias sesión y entras al Home)*... el sistema me asigna un UUID único mediante Firebase. Si yo le diese mi cuenta a un amigo y él entrara ahora mismo, el sistema detectaría el cambio a través de un WebSocket nativo (`onSnapshot`) en Firestore y me expulsaría al instante. Cero cuentas compartidas."


**[Min 2:30] Dashboard y Datos en Vivo:**
> "Una vez dentro, el Dashboard es el centro de control. Arriba tenemos el marcador en vivo. Esto no es estático; mi servidor cruza datos de las APIs de la NBA y NFL para traer los últimos resultados.
> Más abajo, tenemos el feed de noticias. En lugar de copiar textos a mano, mi backend lee directamente el **archivo XML** nativo de ESPN, lo parsea a JSON mediante la librería `cheerio` y lo sirve al frontend. Y gracias a Firestore, si yo ahora mismo escribo un comentario o doy like aquí *(escribes un comentario en la noticia y le das a enviar)*, aparecerá instantáneamente para todos los usuarios conectados sin necesidad de recargar la página."

**[Min 4:00] Ligas y Caché Inteligente:**
> *(Navegas a la sección de Jugadores)*
> "Si quiero buscar a un jugador, entra en juego el rendimiento. Las APIs gratuitas de deportes limitan el número de peticiones, tirando el famoso error 429 de 'Too Many Requests'.
> Para evitar esto, programé un **Proxy Inverso con Caché** en mi servidor Node.js. Cuando yo busco 'LeBron James' aquí, mi servidor descarga los datos y los guarda en la memoria RAM. Si otro usuario busca lo mismo 5 segundos después, la respuesta es inmediata, reduciendo la latencia y protegiendo los límites de las APIs externas."

**[Min 5:00] Comunidad y Dream Team:**
> *(Navegas a tu Perfil / Dream Team)*
> "Como esto es una red social deportiva, cada usuario puede construir su propio equipo soñado. He desarrollado una interfaz multi-liga que adapta dinámicamente el campo visual y las posiciones tácticas según el deporte seleccionado —ya sea Baloncesto NBA o Fútbol Americano NFL—, guardando ambas plantillas de forma independiente en Firestore.
> *(Navegas a Comunidad / Búsqueda)*
> Y si busco a otro usuario de la plataforma, puedo ver su perfil público. Para proteger la privacidad, las peticiones que hago a la base de datos están capadas y solo devuelven los avatares generados dinámicamente o sus plantillas de jugadores, pero nunca credenciales."


---

## ⏱️ FASE 3: PROCESO DE DESARROLLO Y DIFICULTADES (Minuto 6:30 - 8:30)
*Objetivo Rúbrica (2.5 puntos): Fases, problemas técnicos y soluciones aplicadas.*

*(En pantalla: Abre VS Code. Pon el mapa `MAPA_DEFENSA.md` en una pestaña si quieres, pero muestra el código `style.css` y `server.js`).*

**Tú dices:**
> "Llegar a este resultado ha tenido varios retos técnicos importantes durante el desarrollo.
> 
> El primer gran reto fue la **estrategia Dual de Bases de Datos**. Podría haber hecho todo en Firebase, pero quería la robustez relacional de **MySQL** para las contraseñas e identidades. A su vez, el chat en tiempo real habría saturado MySQL, por lo que usé **Socket.io** en mi servidor Node.js para que los mensajes viajen por RAM sin coste, y dejé **Firestore** exclusivamente para la parte social persistente (favoritos y perfiles).
> 
> El segundo gran reto técnico fue la **UI y la deuda técnica**. *(Muestras el style.css rápidamente)*. Aposté por un diseño corporativo basado en 'Dark Glassmorphism'. Esto supuso prescindir de librerías como Bootstrap y escribir más de 6.000 líneas de Vainilla CSS. Para estandarizarlo y evitar que en Android se viera distinto que en Windows, cambié todos los emojis por tipografías vectoriales de *Google Material Icons*. Sé que el CSS no está todo lo modularizado que debería, pero mi decisión de proyecto fue priorizar la estabilidad del Backend y las bases de datos en el tiempo que tenía."

---

## ⏱️ FASE 4: CIERRE Y CONCLUSIÓN (Minuto 8:30 - 9:30)

*(En pantalla: Vuelves a la web, quizá mostrando el chat o la página principal).*

**Tú dices:**
> "Para finalizar, considero que **ProLeague** ha cumplido con nota los objetivos del Producto Mínimo Viable. No es solo un frontend que pinta datos; tiene un backend proxy que cachea peticiones, parsea XML, encripta contraseñas con Bcrypt, procesa subida de imágenes con Multer y sincroniza webSockets.
> 
> De cara a futuro, las principales mejoras serían refactorizar todo el CSS a SASS para reducir la deuda técnica y añadir notificaciones Push para los partidos.
> 
> Ha sido un trabajo muy exigente pero muy enriquecedor. Gracias por su atención, quedo a disposición del tribunal para profundizar en cualquier línea de código o resolver sus dudas."

---

## 💡 CONSEJOS EXTRA PARA EL ENSAYO (Rúbrica de Actitud y Exposición):

1. **Ensaya con Cronómetro:** Tienes que grabar un audio de ti mismo leyendo este guion con tranquilidad. Debería durarte entre 8:30 y 9:30 minutos. Eso te da un margen de 30 segundos si te trabas. Si te pasas de 10:00, te quitarán 1 punto.
2. **Tu lenguaje no verbal (1.5 pts Rúbrica):** No leas la pantalla. El guion es para que te lo aprendas como un monólogo o una obra de teatro. Habla mirando al tribunal y cuando vayas a enseñar algo en la pantalla, señala y di: *"Como pueden ver aquí..."*.
3. **Fluidez (No pidas perdón):** Si durante la demo la API de la NBA falla, **NO TE PONGAS NERVIOSO**. Di: *"Como ven, acabamos de recibir un Error 429 de la API externa de BallDontLie, justo para lo que programé el `apiCache`, aunque al ser una petición nueva no estaba guardada. Son las limitaciones de usar APIs gratuitas en desarrollo."* ¡Les encantará esa madurez técnica!
4. **Al terminar:** Calla, respira y espera las preguntas. Las preguntas que te hagan ya las tienes todas resueltas en el documento `MAPA_DEFENSA.md`.
