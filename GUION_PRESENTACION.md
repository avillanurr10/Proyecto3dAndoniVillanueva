🎙️ GUION DE PRESENTACIÓN: PROLEAGUE (10 MINUTOS EXACTOS)
Este guion está optimizado para destacar tu solvencia técnica ante el tribunal, justificar decisiones de arquitectura y estructurar la demostración de forma fluida.

⏱️ FASE 1: PRESENTACIÓN GENERAL Y APERTURA (Minuto 0:00 - 1:30)
Objetivo: Captar la atención, contextualizar el problema, delimitar el alcance y explicar el valor añadido del proyecto.

(En pantalla: Portada de ProLeague con diseño premium en modo oscuro, o primera diapositiva)

Tú dicen:

"Buenos días, miembros del tribunal. Mi nombre es Andoni Villanueva y hoy les presento ProLeague, una plataforma web diseñada específicamente para centralizar la información deportiva y la interacción social de los aficionados a la NBA y la NFL.

El problema que resuelve: Actualmente, el fan de los deportes sufre de fragmentación de información: tiene que consultar los marcadores en una web, leer las noticias en otra, entrar a redes sociales como X o Discord para debatir, y usar comparadores externos para analizar estadísticas.

Nuestro valor añadido: ProLeague unifica toda esta experiencia en un ecosistema integral interactivo. Un usuario puede consultar resultados en tiempo real, interactuar con noticias actualizadas, debatir en chats efímeros y crear alineaciones personalizadas.

Alcance del proyecto (MVP): El proyecto se ha desarrollado bajo una arquitectura robusta de tres capas: un backend API REST con Node.js y Express, una estrategia de base de datos híbrida (MySQL para seguridad y Firestore para tiempo real), y una interfaz rica desarrollada en Vanilla JavaScript con estética Dark Glassmorphism."

⏱️ FASE 2: DEMO FUNCIONAL E HITOS TÉCNICOS (Minuto 1:30 - 6:30)
Objetivo: Demostrar los flujos principales del sistema de forma ágil y justificar el código en vivo.

(En pantalla: Interactúa dinámicamente con la aplicación web mientras hablas)

🔒 [Min 1:30] Autenticación y Seguridad (Session Guard)
(Muestras la pantalla de Login / Registro. Inicias sesión) Tú dices:

"Comenzamos con el módulo de acceso. He implementado un Login Híbrido que permite entrar indistintamente con correo electrónico o nombre de usuario. Una vez dentro de la sesión...

(Inicias sesión y entras al Home)

...el sistema valida las credenciales y le asigna al cliente un identificador de sesión único. Para evitar la mala práctica de compartir cuentas, he programado un Session Guard (Guardián de Sesión Única). Utilizando un listener en tiempo real de Firestore (onSnapshot), el navegador detecta al instante si el usuario inicia sesión desde otro dispositivo o pestaña, invalidando la sesión antigua y expulsándolo de inmediato para asegurar el control de acceso."

📊 [Min 2:30] Dashboard Reactivo y Parseo de Feeds XML
(Muestras el Carrusel de Partidos y las Noticias en el Home) Tú dices:

"Una vez en el Dashboard, la información cobra vida. En la parte superior contamos con el Scoreboard que muestra los partidos más recientes de la NBA y la NFL; los datos se solicitan a través de nuestro servidor para evitar sobrecargar el navegador.

Si bajamos a la sección de Noticias, vemos que el contenido está actualizado al minuto. Para no depender de bases de datos estáticas que requieran mantenimiento manual, mi backend actúa como un integrador. Consume el feed XML nativo de ESPN, lo parsea a JSON en el servidor utilizando la librería cheerio y lo sirve optimizado al frontend.

Además, los usuarios pueden dar 'Me Gusta' y comentar. Gracias a la reactividad de Firestore, cualquier comentario que escriba ahora mismo... (escribes un comentario rápido de prueba) ...se reflejará instantáneamente en las pantallas de todos los usuarios conectados sin necesidad de recargar."

⚡ [Min 4:00] Buscador de Jugadores y Proxy Cache Server
(Navegas a la sección de Buscador de Jugadores) Tú dices:

"El tercer pilar es el buscador de jugadores. Las APIs deportivas gratuitas suelen imponer límites estrictos de peticiones, respondiendo con errores 429 (Too Many Requests).

Para solucionar esto y optimizar el rendimiento, diseñé un Proxy Inverso con Caché local en el servidor de Node.js. Cuando busco a un jugador por primera vez, por ejemplo 'LeBron James' o 'Patrick Mahomes', mi backend hace la petición al proveedor oficial y almacena el JSON en la memoria RAM del servidor. Las siguientes búsquedas idénticas se resuelven en menos de 100 milisegundos directamente desde la caché, lo que ahorra ancho de banda, protege los límites de uso de la API externa y mejora drásticamente la latencia del cliente."

👥 [Min 5:00] Dream Team: Evolución del MVP y Comunidad
(Navegas a la vista del Dream Team) Tú dices:

"En la sección 'Mi Equipo Ideal', el usuario puede diseñar visualmente su quinteto preferido.

Un detalle clave: En la entrega del MVP inicial, la asignación de jugadores en las posiciones del campo era flexible. Sin embargo, en el desarrollo continuo posterior a la entrega y de cara a esta demo, he integrado un validador de compatibilidad de posiciones en el frontend. Si intento colocar a un Pivot (C) como Base (PG)... (haces el intento rápido en pantalla de colocar a un Pivot en la ranura de Base) ...el sistema intercepta la acción, valida la incompatibilidad cruzando el JSON del jugador con las reglas del campo, y lo bloquea amigablemente con una alerta tipo toast.

(Navegas al buscador de usuarios y entras a un Perfil Público)

Por último, en la sección de Comunidad podemos inspeccionar a otros usuarios. Siguiendo estándares de seguridad informática y privacidad de datos, las llamadas a Firebase están estrictamente filtradas para que solo devuelvan la información de su Dream Team y avatares públicos, protegiendo por completo sus credenciales de acceso."

⏱️ FASE 3: DETALLES DE INGENIERÍA Y TRABAJO CON CSS (Minuto 6:30 - 8:30)
Objetivo: Justificar la toma de decisiones técnicas complejas, el diseño visual y reconocer las áreas de mejora de forma madura.

(En pantalla: Abre VS Code. Muestra brevemente server.js y style.css)

Tú dices:

"El desarrollo del proyecto me enfrentó a retos técnicos clave que requirieron decisiones de ingeniería:

Estrategia Híbrida de Almacenamiento: Decidí usar MySQL para gestionar el registro de usuarios debido a su alta fiabilidad transaccional e integridad referencial, encriptando contraseñas con bcrypt. Por otro lado, la interacción en tiempo real y el volumen de datos de los perfiles sociales se delegaron a Firestore, reduciendo la carga y la complejidad del servidor Node.js.

Interacciones Efímeras sin Coste: Para el chat de la comunidad, en lugar de usar Firestore —que habría incrementado la factura de lectura/escritura exponencialmente— implementé WebSockets con Socket.io. Los mensajes efímeros se distribuyen en tiempo real en la memoria RAM del servidor de forma totalmente gratuita y segura, aplicando además filtros de sanitización para prevenir ataques XSS de inyección de código.

UI Corporativa y Deuda Técnica: El aspecto visual de la aplicación se construyó utilizando una hoja de estilos Vanilla CSS personalizada de más de 6,000 líneas para lograr la estética de Dark Glassmorphism. Para asegurar la homogeneidad visual en cualquier dispositivo, reemplacé los emojis tradicionales por la fuente vectorial Google Material Icons. Soy consciente de que una hoja de estilos de este tamaño acumula deuda técnica, por lo que la migración a un preprocesador como SASS y la modularización en componentes es una prioridad establecida para la siguiente versión."

⏱️ FASE 4: CONCLUSIÓN Y CIERRE (Minuto 8:30 - 9:30)
Objetivo: Finalizar con fuerza, proponer mejoras futuras y abrir el turno de preguntas.

(En pantalla: Regresa a la página principal de la aplicación o al Dashboard)

Tú dices:

"Para concluir, ProLeague cumple rigurosamente con los objetivos del Producto Mínimo Viable. No es solo una página estática; es una aplicación web interactiva que integra pasarelas proxy con caching, encriptación en backend, almacenamiento híbrido, sockets bidireccionales y actualizaciones reactivas.

Como líneas de desarrollo futuro, además de la refactorización CSS, está planificada la integración de notificaciones push móviles para avisar de partidos en juego e incorporar un sistema de ligas de fantasía interactivo entre usuarios.

Ha sido un proyecto desafiante pero sumamente instructivo. Agradezco su atención y quedo a su total disposición para responder a sus preguntas o profundizar en cualquier sección de la arquitectura o el código."

💡 CONSEJOS EXTRA DE ACTITUD PARA ANDONI:
Enfoca el fallo a tu favor: Si el tribunal notase la diferencia entre el proyecto subido y el de la demo, explícalo como proactividad: "Efectivamente, el MVP entregado cumplía con las especificaciones iniciales y dejaba la asignación abierta, pero como parte del ciclo de vida continuo del software e interactuando con los usuarios, decidí añadir esta mejora de validación para enriquecer la demo que hoy les presento." ¡Es una respuesta digna de un desarrollador Senior!
Usa atajos de teclado: Cuando saltes de la web al código en la fase 3, usa Alt + Tab o Ctrl + P en VS Code. La fluidez visual transmite confianza.
