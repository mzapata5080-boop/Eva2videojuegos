# Dragon ball Adventure
juego de plataformas desarrollado en unity y c#
goku tiene como objetivo reunir las 7 esferas repartidas por el mapa para ganar contando con 3 vidas para completar el objetivo

# Plataforma usada
-Windows 11

# Instrucciones de ejecucion
- descargar el proyecto y abrirlo mediante unity, ejecutar mediante la prueba dentro de unity o seleccionar Files->Build Profiles->Build(Parte inferior derecha de la venta que se abre), luego de que cargue ejucutar el .exe y listo

# Controles
Movimiento: AWSD o flechas direccionales
salto: espacio
ataque: letra F
pausar: esc

# Mecanicas implementadas
movimiento
-control de fisicas y saltos con rigidvbody2d
-deteccion de suelo medianre Physics2D.Raycast
-orientacion del personaje mediante la rotacion de escala
Sistema de ataque
-ataque mediante proyectil(esfera de energia) con tiempo de vida y coldown para evitar el spam de ataques
-destruccion del terreno "tilemapHielo" mediante el ataque
Sistema de vidas y daño
-contador de 3 vidas con representación en pantalla mediante UI
-la lava esta configurada para que descuente vida al jugador y aplique un rebote vertical a este
-el jugador es invulnerable un tiempo pequeño despues de recibir daño
Fondo de pantalla del nivel
-el fondo de pantalla captura el movimiento de la camara para seguir al jugador

# Tecnologías
-Motor grafico: Unity 6000.2.3f1 2D
-Lenguaje de programación: C#
-render pipeline: Universal render pipeline (URP 2D)
-fisicas:Unity physics 2D(rigidbody2D),boxcollider2D, compositecollider2D
-interfaz: textmeshpro

# Dependencias
-PlayerMovement.cs: Control del jugador
-GameManager.cs: Control de la partida, UI de esferas totales, vida, pausa y victoria
-Proyectil.cs: Desplazamiento del ataque y destrucción de los bloques de hielo
-DanioLava.cs: Deteccion de coliciones con el jugador para restar vidas al jugador
-EsferaDragon.cs: logica de obtencion de esferas y notificación mediante el GameManager
-MenuPrinicpal.cs: Menu principal y carga de escena
-SeguirCamaraFondo.cs: Control de la posicion de la imagen de fondo mediante el movimiento de la camara

# Riesgo y mitigacion
-riesgo, perdida de deteccion del suelo con saltos infinitos--Mitigación implementacionde raycast verditacl descendente para validar la colision con la capa suelo
-riesgo, traspaso de plataformas o caidas al vacio--Mitigacion se configuro un rigidbody2D para el personaje con deteccion de coliciones en como continuo y uso de bordes continuos para los bordes solidos

# Roles del desarrollo
- desarollado totalmente por Maximiliano Andrés Badilla Zapata
