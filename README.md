# Lernperiode-2

# Grob-Planung 
- [ ] Eigene Projekte
- [ ] Modul 431 Fertig
- [ ] Machine Learning

## Leit-Satz

In dieser neuen Lernperiode hoffe ich, einige ein wenig fortgeschrittenere Projekte zu machen. Einige meiner Ideen wären zum Beispiel: Etwas mit machine Learning automatisieren; Ein eigenes Spiel zu Programmieren; Ein Projekt mit Face-Recognition und anderes. Vermutlich werden die Projekte mit Machine Learning und Face Recognition in Python sein, da dies ja die Sprache für das ist. Ein eigenes Spiel werde ich sehr wahrscheinlich mit der Godot-engine machen. Ich weiss noch nicht ob ich mit all dem fertig werde, aber ich hoffe es stark. Was ich auch noch zu lernen hoffe ist effizienter zu arbeiten, aber das ist mehr so ein nebenziel.

# Dienstag 24/10/23
## Zusammenfassung
Heute habe ich nicht programmiert. Ich habe heute nur an dem Modul 431 gearbeitet, da wir dieses am Mitwoch diese Woche bewertet wird. Ich finde ich habe gut gearbeitet, aber manchmal hatte ich konzentrationsschwierigkeiten.

## Arbeitspakete für nächstes mal
- [x] Die Programmierumgebung von Godot einrichten.
- [x] 2D Platformer Grundlagen Programmieren
- [x] Grafiken finden
- [x] tutorial finden


# Dienstag 31/10/23
## Zusammenfassung
Heute habe ich Godot eingerichtet, die Grundlagen gelernt und gelegt, herausgefunden wo ich meine Grafiken hernehmen kann und ein gutes Tutorial für ein einfaches Spiel gefunden. Ich denke aber, ich mache als erstest nicht einen Plattformer sondern etwas anderes. 

## Arbeitspakete für nächstes mal
- [ ] Ein menü machen
- [ ] funktionierende Gebäude
- [ ] UPS grundberechnung machen. (Unit per second)

# Dienstag 14/11/2023
## Zusammenfassung
Heute habe ich an unserem Informatik-Auftrag für Herrn Hadorn weitergearbeitet. Ich habe den Code geschriben, um die CSV Datei mit den Länderdaten einzulesen und zu einem Dictionary umzuwandeln. Zudem habe ich ein bewertungssystem programmiert um die jeweilige übereinstimmung mit den Nutzeranforderungen zu bewerten.

## Arbeitspakete für nächstes mal
- [ ] GUI
- [x] Bugfixes
- [x] Input-Kontrolle

# Dienstag 21/11/2023
## Zusammenfassung
Ich habe noch kein GUI erstellt, dafür habe ich aber die Input-Kontrolle abgeschlossen. Danach traten aber noch einige unerwartete und mir unerklärbare Bugs aufgetaucht. Deshalb habe ich länger für die Bugfixes gebraucht. Diese sind nun jedoch grösstenteils abgeschlossen.
## Arbeitspakete für nächstes mal
- [X] GUI
Ich denke das GUI wird recht anspruchsvoll sein, da ich will das es auch gut aussieht. Deshalb habe ich noch nicht mehr eingeplant.

# Dienstag 12/12/2023
## Zusammenfassung
Heute habe ich am Advent of Code weitergearbeitet, hänge jedoch fest. Ich habe versucht meinen Code für den Teil 2 von Tag 1 zum Laufen zu bringen, aber es will enfach nicht. Zudem habe ich an der Aufgabe mit den Zahnrädern gearbeitet, bin jedoch noch nicht fertig.
## Arbeitspakete für nächstes mal
- [ ] PlayerCharacter
- [ ] Hindernisse
- [ ] Leben

# Dienstag 19/12/2023
## Zusammenfassung
Heute habe ich an meinem Spiel gearbeitet. Ich bin nicht so weit wie ich sein könnte. Ich habe keine Motivation mehr für meine erste Spiel-Idee gefunden, also habe ich mein Projekt gewechselt. Ich bin nun gerade daran ein altes Scratch-Spiel neuzuprogrammieren, natürlich in einer weniger visuellen Game-Engine(Godot). Meine "neue" Spiel-idee ist folgende: Ein 2D Dungeoncrawler, die Hauptfigur ist ein Ritter mit verschiedenen Pistolen. Es gibt Räume, sobald jedes Monster besiegt ist öffnet sich die Tür zum nächsten Raum. Aber nun zu meinen heutigen Aktivitäten. 
Da Ich mir in Godot und GDScript noch nicht so sicher bin, habe ich ein tutorial hinzugezogen.(https://docs.godotengine.org/en/stable/getting_started/first_2d_game/02.player_scene.html)
Ich habe es geschaft, dem Player Health zu geben und diese abzuziehen wenn er in ein aktives Hinderniss läuft. 
```GDScript
func _on_area_2d_body_entered(body):
	if body.name != "level_walls" and body.name != "CharacterBody2D":
		print(body.name)
		health -= 1
		if health <= 0:
			die()
```
Um zu verhindern, das Leben abgezogen werden wenn das Hinderniss nicht aktiv ist(das Hinderniss ist in meinem Falle Stacheln, die alle 4 Sekunden aus dem Boden kommen) war es notwendig die Hitbox des Hindernisses zu deaktivieren wenn es nicht mehr aktiv ist.
```GDScript
func cycle_state():
	currentState = 0
	$obstacle/obstacleHitbox.disabled = true
	$obstacle/obstacleTexture.play("ok")
	await get_tree().create_timer(2).timeout
	currentState = 1
	$obstacle/obstacleTexture.play("warning")
	await get_tree().create_timer(2).timeout
	currentState = 2
	$obstacle/obstacleHitbox.disabled = false
	$obstacle/obstacleTexture.play("nok")
	await get_tree().create_timer(2).timeout
```
Ich hatte auch probleme damit, ein Objekt festzulegen, zu welchem der Spieler am anfang des Spiels gehen soll. Dieses Objekt soll flexibel positionierbar sein. Aber da der Spieler und das Objekt in einer unterschiedlichen Szene sind, konnte ich nicht einfach ```position = $Marker2D.position``` ausführen, da dieses objekt nicht in der selben Szene ist. Ich habe dieses Problem dadurch gelöst, das ich eine Start-Funktion im player definiere, die als Argument eine Position nimmt und die Position des Spielers auf die gegebene setzt.
```GDScript
func start(pos):
	position = pos
	show()
	$CharacterBody2D/Area2D/CollisionShape2D.disabled = false
```
Diese funktion wird aber vom Spieler selbst nie ausgeführt. Ich führe diese funktion von der Game-Szene aus aus. Dies funktioniert, da ich in der Game-Szene ein weiteres Skript habe, welches Zugriff auf beide Objekte, also den Spieler und die Startposition hat. Von dort aus kann ich folgenden Code ausführen:
```GDScript
# Called when the node enters the scene tree for the first time.
func _ready():
	$Player.start($Marker2D.position)
```

## Reflektion
Ich denke, dass meine Arbeitsweise in Ordnung ist, allerdings habe ich nicht ganz so viele Durchhaltewille falls es mich nicht so interressiert. Bei der Analyse meiner Arbeitsweise habe ich folgendes entdeckt: Ich mache sehr wenig Pausen, obwohl manchmal meine Konzentration nachlässt, wenn ich ein Problem habe, oder einen Fehler, schaue ich mir an ob es ein logischer fehler ist, falls nicht google ich es einfach direkt, ich mag es nicht sonderlich alles strikt nach Tutorial zu machen, ich möchten meinen eigenen Code schreiben. Verbesserungsvorschläge die ich mir selbst geben kann sind folgende: Mehr Pausen machen, auch wenn es wirkt als ob man dadurch Zeit verliert ist man danach wieder schneller. Bei einem Fehler vielleicht mehr als einmal schauen ob ich ihn selbst lösen kann, ich denke falls man den Fehler dann findet lernt man mehr daraus als wenn man es googelt(allerdings auch nicht Stunden an einem Fehler verbringen)

