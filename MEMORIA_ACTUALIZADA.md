# MEMORIA TÉCNICA: PROLEAGUE

## 1. Portada
**Alumno:** Andoni Villanueva Urrestarazu  
**Ciclo:** Desarrollo de Aplicaciones Multiplataforma — 2º curso  
**Proyecto:** ProLeague — Plataforma de Análisis Deportivo NBA/NFL  
**Centro:** Maria Ana Sanz  
**Curso Académico:** 2025-2026

---

## 2. Índice
1. Portada
2. Índice
3. Resumen / Abstract
4. Descripción y justificación del proyecto
5. Objetivos del proyecto
6. Recursos hardware, software y arquitectura del proyecto
7. Fases del desarrollo
8. Conclusiones
9. Bibliografía y referencias

---

## 3. Resumen
ProLeague es una plataforma web avanzada diseñada para el análisis, seguimiento y dinamización comunitaria de las dos grandes ligas deportivas estadounidenses: la NBA y la NFL. El objetivo principal era crear una herramienta centralizada que combine datos estadísticos reales con una experiencia social moderna. La aplicación permite consultar clasificaciones vivas, resultados recientes, noticias de última hora, y ofrece herramientas exclusivas como un constructor de "Dream Teams", comparadores de jugadores con gráficos interactivos y un chat persistente.

El sistema utiliza un backend en Node.js que funciona como proxy seguro y sistema de caché, garantizando la fluidez de los datos de APIs externas. El frontend destaca por una estética oscura y profesional, priorizando siempre la experiencia de usuario. El resultado final ha superado el Producto Mínimo Viable, logrando una herramienta integral y escalable que aplica una arquitectura similar a la utilizada en entornos profesionales reales. Considero que es un ámbito con gran potencial de evolución y ampliación funcional futura.

**Abstract:**
ProLeague is an advanced web platform designed for the analysis, monitoring, and community engagement of the two major American sports leagues: the NBA and the NFL. The project's main purpose is to provide a centralized tool that combines real-time statistical data with a modern social experience. The application allows users to consult live standings, recent game scores, breaking news, and offers exclusive tools such as a "Dream Team" builder, player comparison features using interactive charts, and a persistent live chat system.

The application uses a Node.js backend acting as a secure proxy and caching system, ensuring data fluidity from international APIs. The frontend, featuring a dark/glassmorphism aesthetic, prioritizes user experience and data visualization. The results have exceeded the Minimum Viable Product, achieving a comprehensive and scalable tool.

---

## 4. Descripción y justificación del proyecto
ProLeague nace para cubrir el vacío entre las aplicaciones de resultados simples y las plataformas de apuestas, centrándose en el análisis de rendimiento deportivo y la interacción social orientada a la comunidad, y evitando un deporte frecuente como el fútbol, dando más recursos informativos sobre otros deportes.

### 4.1. Justificación de la necesidad
- **Análisis Visual:** Transforma tablas de números en gráficos interactivos.
- **Interacción Social:** Chat persistente y perfiles públicos para debatir sobre deporte.
- **Eficiencia:** Unifica múltiples fuentes de datos (ESPN, BallDontLie) en un solo dashboard.

### 4.2. Comparativa con soluciones existentes
(Consultar tabla comparativa para detalles sobre Gráficos, Dream Team e Interacción Social).

---

## 5. Objetivos del proyecto
### 5.1. Producto Mínimo Viable
- **Autenticación:** Registro e inicio de sesión con verificación de email.
- **Datos en Vivo:** Clasificaciones y resultados NBA/NFL en tiempo real.
- **Noticias:** Feed de noticias RSS actualizado al minuto.
- **Chat:** Sala general de comunicación mediante WebSockets.

### 5.2. Ampliaciones o mejoras
- **Login Híbrido Avanzado:** Acceso mediante Email o Nombre de Usuario indistintamente.
- **Asistente Coach Pro:** Mascota virtual animada con consejos dinámicos y soporte UX.
- **Comunidad Limpia:** Sistema de búsqueda con triple filtrado y prevención de duplicados.
- **Seguridad Reactiva:** Session Guard para control de sesión única.
- **Estandarización UI/UX:** Interfaz profesional mediante la integración de **Google Material Icons**, eliminando emojis nativos para garantizar una visualización corporativa y coherente en todos los sistemas operativos. Link

---

## 6. Recursos hardware, software y arquitectura del proyecto
### 6.2. Arquitectura del proyecto
**A. Almacenamiento de Datos (Dual-Storage Strategy)**
- Capa Relacional (MySQL): Gestión de usuarios e integridad de la cuenta.
- Capa NoSQL (Cloud Firestore): Persistencia social (Dream Teams, equipos favoritos, historial de chat e interacciones).

**B. APIs y Servicios Externos**
- BallDontLie API & ESPN API / RSS.
- Firebase Auth & Firestore REST.

**C. Comunicación entre Componentes**
- Protocolo HTTPS (REST API) con sistema de caché en memoria para evitar errores 429.
- Protocolo WebSockets (Socket.io) para comunicación bidireccional en tiempo real.

---

## 7. Fases del desarrollo
### 7.3. Hitos de Ingeniería
- **Abstracción de APIs:** Sistema de caché `apiCache` en Node.js que reduce la latencia de ~1.2s a <150ms.
- **Login REST-Auth:** Buscador de emails vía REST API para habilitar el login híbrido.
- **Coach Pro AI:** Componente animado con diseño Glassmorphism y soporte UX.
- **D. Normalización Estética e Iconografía Estándar:** Sustitución de activos visuales informales por iconografía vectorial (Material Icons), garantizando una identidad visual sólida y profesional.

---

## 8. Conclusiones
### 8.1. Logros Técnicos y Objetivos Alcanzados
Se han cumplido satisfactoriamente todos los objetivos planteados. El mayor logro técnico ha sido la creación de una arquitectura de datos híbrida y la implementación de un middleware de caché que profesionaliza el uso de APIs públicas gratuitas.

### 8.2. Retos Superados y Aprendizaje
El principal desafío fue la normalización de datos entre múltiples proveedores. A nivel personal, este proyecto ha servido para consolidar conocimientos en asincronía avanzada, seguridad en tiempo real (Session Guard) y diseño UX/UI.
**Autocrítica y Deuda Técnica (CSS):** Un reto identificado ha sido la gestión del diseño visual. El archivo `style.css` ha alcanzado una dimensión considerable. Como aprendizaje para futuras iteraciones, reconozco la necesidad de migrar hacia arquitecturas modulares (como SASS) para mejorar la mantenibilidad. Se priorizó la estabilidad visual del sistema frente a una refactorización completa para evitar regresiones antes de la entrega.

---

## 9. Bibliografía y referencias
- Node.js, Express, Socket.io y Chart.js Documentation.
- Firebase Auth & Firestore Documentation.
- **Google Material Icons Library (2024):** Estándares de iconografía para interfaces modernas.
- BallDontLie & ESPN API Reference.
- RSS Board Spec y MDN Web Docs.
