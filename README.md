# Documentatie - Jumper Opdracht
<em>Door: Anass, Berat, Zakaria</em>

Voor deze tutorial gaan we een zelflerende agent maken die getraind wordt om obstakels te ontwijken door er overheen te springen. Elke episode krijgen de obstakels een andere snelheid (de minimum- en maximumsnelheid zijn aanpasbaar). De agent staat in het midden van een kruispunt en moet obstakels vermijden die vanuit twee richtingen kunnen komen.

<h2>Voorbereiding</h2>
<ol>
  <li>
      <ul>
        <li>Maak een nieuw 3D project aan.</li>
        <img src="https://user-images.githubusercontent.com/43028431/233731064-81fcf247-f9f8-4ad5-8fff-f83cb48e69f2.png">
      </ul>
  </li>
  <li>
      <ul>
        <li>Installeer ML Agents via de Package Manager in Unity (Window -> Package Manager). Selecteer Unity Registry van de packages drop down menu links boven. Zoek naar ML Agents en klik op Install. Hierdoor wordt ML Agents geïnstalleerd in Unity.</li>
        <img src="https://user-images.githubusercontent.com/43028431/233704119-bbb73afa-270b-4c2f-bdd8-15c285b11289.png">
      </ul>
  </li>
  <li>
      <ul>
        <li>Maak volgende bestanden (Rechtermuisknop - Create - Folder): Prefabs(vooraf gemaakte game-objects), Scripts, Skins</li>
        <img src="https://user-images.githubusercontent.com/43028431/233711440-7ca354e9-5e07-454d-9b7b-df02a1e9ac3e.png">
      </ul>
  </li>
  <li>
      <ul>
        <li><ins>Optioneel:</ins> Download van de <a href="https://assetstore.unity.com/">Asset Store</a> het volgende: <a href="https://assetstore.unity.com/packages/3d/environments/urban/modular-lowpoly-streets-free-192094">Modular Lowpoly Streets (Free)</a> </li>
        <li>Dit stelt je in staat om vooraf gemaakte objecten, zoals een Kruispunt-prefab, te gebruiken in je scene.</li>
      </ul>
  </li>
</ol>

<h2>Bouw je scene</h2>
  <dl>
    <dt>Onze scene ziet er zo uit:</dt>
        <img src="https://user-images.githubusercontent.com/43028431/233704460-5f34a304-0e57-4dee-95c8-ce9941e3b30c.png">
    <dd>We hebben een Agent, een beloningsobject (in dit geval is het een coin), obstakels die tegen ons proberen te botsen. Hoe de scene eruitziet mag je volledig zelf kiezen.</dd>
  </dl>

<h2>Opstelling/eigenschappen van de objecten:</h2>
Tips:
<ul>
  <li>Objecten maken: Bij de Hierarchy - Rechtermuisknop - 3D-object - Object kiezen</li>
  <li>Scripts maken in mapje "Scripts": Rechtermuisknop - Create - C# Script</li>
  <li>Kleuren maken in mapje "Skins": Rechtermuisknop - Create - Material</li>
  <li>Componenten toevoegen: Kies een object - Inspector - Add Component</li>
</ul>
<ol>
  <li>Agent
      <ul>
        <li>De agent is in dit voorbeeld een kubus en heeft de volgende componenten:</li>
        <li>
          <ul>
            <li>Rigidbody: Een component in Unity die zwaartekracht en andere fysieke krachten simuleert op een gameobject, waardoor het realistische bewegingen kan maken.</li>
            <li>Behavior Parameters: Een component in Unity die de parameters instelt die van invloed zijn op het gedrag van een AI-agent.</li>
            <li>Decision Requester: Een component in Unity die een AI-agent aanstuurt en beslissingen neemt op basis van de input van sensoren en gedrag.</li>
            <li>Box collider: Een component in Unity die een onzichtbaar rechthoekig gebied creëert rondom een gameobject, waardoor het kan interageren met andere objecten in de game.</li>
            <li>Ray Perception Sensor 3D: Een component in Unity die een AI-agent in staat stelt om de afstand en richting van objecten te detecteren met behulp van laserstralen, waardoor het betere beslissingen kan nemen.</li>
            <li>Note: Wanneer je de script toevoegd moet je de coin-object slepen naar het deel "Coin"</li>
          </ul>
        </li>
        <img src="https://user-images.githubusercontent.com/43028431/233707589-f571cdff-01aa-428c-8dfc-b0370333fac4.png">
        <img src="https://user-images.githubusercontent.com/43028431/233707657-2648e4eb-0d69-468e-9c5c-40f2905dea85.png">
        <img src="https://user-images.githubusercontent.com/43028431/233707799-3b0e824b-f59f-41cc-a58f-f8b3feeb609d.png">
      </ul>
  </li>
  <li>Coin
      <ul>
        <li>De coin is een cylinder en heeft de volgende componenten:</li>
        <li>
          <ul>
            <li>Capsule Collider: Een component in Unity die een onzichtbaar capsulevormig gebied creëert rondom een gameobject, waardoor het kan interageren met andere objecten in de game. Het wordt vaak gebruikt voor het detecteren van botsingen tussen personages en objecten.</li>
            <li>De coin maakt ook gebruik van een "coin"-tag. Deze kan worden gemaakt door: Inspector - Tag dropdown - Add Tag - Tags uitklappen - "+" klikken - Naam toevoegen - Terug naar de inspector van de coin gaan - "coin"-tag toevoegen bij Tags dropdown</li>
          </ul>
        </li>
        <img src="https://user-images.githubusercontent.com/43028431/233713199-11ca9c7c-a1e5-4db5-b669-294009b1dd2f.png">
      </ul>
  </li>
  <li>Wegen
      <ul>
        <li>De wegen waaruit de obstakels spawnen. Deze komen van "Modular Lowpoly Streets (Free)"-pack en heeft de volgende componenten (Of als je deze pack niet wilt gebruiken kan je gebruik maken van een "plane"-object.):</li>
        <li>
          <ul>
            <li>Deze objecten maken gebruik van een "floor"-tag. Deze kan worden gemaakt door: Inspector - Tag dropdown - Add Tag - Tags uitklappen - "+" klikken - Naam toevoegen - Terug naar de inspector van de wegen gaan - "floor"-tag toevoegen bij Tags dropdown</li>
          </ul>
        </li>
        <img src="https://user-images.githubusercontent.com/43028431/233714300-5911e46b-6834-4eda-8d53-a51734920f81.png">
        <li>Hierin zit de spawner, de locatie mag je zelf kiezen.</li>
      </ul>
  </li>
  <li>Obstakel
      <ul>
        <li>Dit is het obstakel zelf, dit moet de agent ontwijken. De obstakel is een cilinder en heeft deze componenten:</li>
        <li>
          <ul>
            <li>Prefab maken: Maak het object en geef het een kleurtje (Mapje "Skins" - Rechtermuisknop - Create - Material - Bij inspector: de kleur kiezen), sleep het object naar het mapje "Prefabs en verwijder het uit te hierarchy."</li>
            <li>Deze prefab maakt gebruik van een "obstacle"-tag. Deze kan worden gemaakt door: Inspector - Tag dropdown - Add Tag - Tags uitklappen - "+" klikken - Naam toevoegen - Terug naar de inspector van de obstakel gaan - "obstacle"-tag toevoegen bij Tags dropdown</li>
          </ul>
        </li>
        <img src="https://user-images.githubusercontent.com/43028431/233720323-fbeb4d18-65cd-461e-b79b-5d4d225af266.png">
        <img src="https://user-images.githubusercontent.com/43028431/233720438-5c5ce4d3-2da6-458f-bdd0-74f6a0e51db9.png">
      </ul>
  </li>
  <li>Obstakel Spawner
      <ul>
        <li>De spawner bestaat uit een empty game object waaraan we een script aan hangen dat ervoor zorgt dat er vanaf de locatie waar het game object wordt geplaats, een obstakel stuurt waarvan jij een prefab van hebt gemaakt:</li>
        <img src="https://user-images.githubusercontent.com/43028431/233721358-df41ef19-a02b-403a-ae55-1de75f1e6c1b.png">
        <li>Wanneer je de script toevoegd moet je de obstakel-prefab slepen naar het deel "Prefab"</li>
      </ul>
  </li>
</ol>

<h2>Scripts</h2>
We maken gebruik van 2 scripts: 1 voor onze agent en 1 voor de empty game object om obstakels te spawnen.
<h4>Agent script</h4>
Deze packets heb je nodig om de script doen te werken:
<ul>
  <li>
    <img src="https://user-images.githubusercontent.com/43028431/233722522-1f38cd16-2435-4080-81aa-38e291ba5563.png">
  </li>
</ul>
De variabelen met de waardes die we nodig hebben:
<ul>
  <li>
    <img src="https://user-images.githubusercontent.com/43028431/233722831-c9944f85-a50f-4b3c-975b-d7bffe11fb0b.png">
  </li>
</ul>
De eerste methode "Initialize()" wordt aangeroepen wanneer de agent wordt geïnitialiseerd en initialiseert de variabele "rb" met de Rigidbody component van het game object waaraan het script is gekoppeld.

De tweede methode "OnEpisodeBegin()" wordt aangeroepen wanneer een nieuwe episode van het spel begint en reset de positie en oriëntatie van de agent als de agent gevallen is of een botsing heeft gehad. Daarnaast verwijdert deze methode alle game objects met de tag "obstacle", zet de boolean variabele "collides" op false, zet de boolean variabele "isGrounded" op true en verplaatst de munt (coin) naar een nieuwe willekeurige positie in de spelomgeving.
<ul>
  <li>
    <img src="https://user-images.githubusercontent.com/43028431/233722883-1d9ff927-dc45-49e8-b1e1-79fc0104e0ad.png">
  </li>
</ul>
De eerste methode "CollectObservations(VectorSensor sensor)" wordt gebruikt om observaties te verzamelen van de huidige toestand van de agent en voegt de positie van de agent toe aan de observaties.

De tweede methode "OnActionReceived(ActionBuffers actionBuffers)" wordt aangeroepen wanneer de agent acties ontvangt van de beslissingsmodule en zorgt voor de beweging van de agent. Het leest de acties en vertaalt deze in een controle-signaal. Vervolgens wordt dit signaal toegepast op de positie van de agent door middel van het toevoegen van een kracht aan de rigidbody van de agent. Er wordt ook gecontroleerd of de agent een obstakel vind (bepaalde lengte), waarbij de agent alleen mag springen als hij op de grond staat en de jump timer is verlopen. Als de munt (coin) is verzameld, wordt de beloning verhoogd en wordt de munt op een nieuwe willekeurige positie geplaatst. Als de agent valt of een botsing heeft, wordt de beloning verlaagd en wordt de episode beëindigd.
<ul>
  <li>
    <img src="https://user-images.githubusercontent.com/43028431/233723213-6a679d1a-24c4-43e1-ad46-f86d12b39e96.png">
  </li>
  <li>
    <img src="https://user-images.githubusercontent.com/43028431/233723280-4e408d89-d582-4491-bf7c-f953f7e596c6.png">
  </li>
</ul>
De Heuristic-methode wordt gebruikt om input van de speler te krijgen en deze te gebruiken om de agent te laten bewegen en springen. Het neemt een ActionBuffers-object en stelt de continue acties in op basis van de input van de speler. Als de speler op de spatiebalk drukt en de agent op de grond staat en de timer voor het springen is verlopen, voegt het een kracht toe aan de rigidbody om de agent te laten springen.

De OnCollisionEnter-methode wordt gebruikt om de grond en obstakels te detecteren. Als de agent de grond raakt, wordt de isGrounded-variabele op true gezet en de jumpTimer op 0 gezet. Als de agent een obstakel of de vloer raakt, wordt de collides-variabele op true gezet. Deze variabele wordt gebruikt in de OnEpisodeBegin-methode om te bepalen of de agent opnieuw moet beginnen als deze is gevallen of een botsing heeft gehad
<ul>
  <li>
    <img src="https://user-images.githubusercontent.com/43028431/233724039-59bc7932-f7f7-44b7-98dd-6dfb96781b87.png">
  </li>
</ul>
<h4>Spawner script</h4>
Deze packets heb je nodig om de script doen te werken:
<ul>
  <li>
    <img src="https://user-images.githubusercontent.com/43028431/233724394-1a024443-895a-476c-b6b9-8410b1f7f389.png">
  </li>
</ul>
De variabelen met de waardes die we nodig hebben:
<ul>
  <li>
    <img src="https://user-images.githubusercontent.com/43028431/233724436-8bb599ce-7aa9-401e-8aa2-fbb62edf2e08.png">
  </li>
</ul>
Deze code is verantwoordelijk voor het spawnen van een prefab object in de scene. De functie Update() wordt elke frame aangeroepen en houdt bij hoeveel tijd er is verstreken sinds de laatste spawn van het prefab object. Als de cooldown voorbij is, wordt de functie SpawnPrefab() aangeroepen. Deze functie maakt een kopie van de prefab op de locatie van de spawner en geeft deze een willekeurige snelheid en schaal. De rotatie van het prefab object wordt ook aangepast voordat het object na 5 seconden wordt vernietigd met de Destroy() functie.
<ul>
  <li>
    <img src="https://user-images.githubusercontent.com/43028431/233724533-be2159b5-bbab-4fcd-bf6a-75e0a7aef9eb.png">
  </li>
</ul>
<h2>Trainen van de agent</h2>
Om te beginnen moet je ervoor zorgen dat je de laatste versie of een long-term support versie van Unity hebt geïnstalleerd. Dit kan worden gedownload vanaf de officiële <a href="https://unity.com/download">Unity-website</a>.

<ul>
  Daarnaast moet je ook Anaconda Prompt hebben geïnstalleerd en bij het runnen ervan voeg je het volgende toe:
  <li>pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html</li>
  Hiermee wordt PyTorch geïnstalleerd, dat nodig is voor het trainen van de A.I.</li>
</ul

Ten slotte moet je ML Agents installeren door het volgende commando in Anaconda Prompt te typen:
- python -m pip install mlagents==0.30.0

Ga dan naar het mapje waar je Unity-project zit en maak je een mapje aan "config" en maak je hierin nog eens een "YAML"-file met de volgende eigenschappen (Het is wel belangrijk dat de naam van je YAML overeenkomt met lijn 2):
![image](https://user-images.githubusercontent.com/43028431/233726189-f40f112c-2626-48a5-96cf-e02b1d8f9309.png)

Als je de A.I. wilt trainen met Anaconda, moet je naar de projectmap (command: cd "pad naar je project") gaan. Typ vervolgens het volgende commando in Anaconda Prompt:
- mlagents-learn config/(naam).yaml --run-id=(Kan je kiezen)

Dit commando start het trainingsproces van de A.I. Je krijgt de boodschap "Start training by pressing the Play button in the Unity Editor." Druk nu op Play in Unity en de agent wordt getraind. Je krijgt een samenvatting van het leerproces te zien.

<h2>Bekijken van de trainingsvoortgang met TensorBoard</h2>
Om de trainingsvoortgang te bekijken, kun je TensorBoard gebruiken. Open een nieuwe Anaconda-terminal, ga naar de projectmap en typ het volgende commando:
- tensorboard --logdir results

Surf vervolgens naar localhost:6006 voor een webinterface waar je een overzicht van de trainingsprocess kunt zien.

<h2>Onze tensorboard resultaten</h2>
De agent had heel lang de tijd gekregen om te leren (1M stappen) en had in het begin (0-100k stappen) een negatieve waarde (max 0.4), maar naarmate de tijd begon hij steeds beter te worden. Af en toe een terugval, maar herpakte zich al snel.

![image](https://user-images.githubusercontent.com/43028431/233727106-30718d59-034d-4981-a83d-9cafd19684c8.png)
