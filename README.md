# Gestion des Places de Parking

Application codée en C# Razor Pages
 
## Introduction

Le plateau indus possède un certain nombre de collaborateurs, mais le problème est que le nombre de places de parkings sont limitées.<br/>
Il y a en effet <b>4</b> places de parking disponible pour le plateau.<br/>
- Les collaborateurs doivent donc s'organiser pour réserver des créneaux sur la semaine courante.<br/>
Un utilisateur lorsqu'il réserve, peut supprimer sa propre réservation après coup s'il le désire en cliquant dessus.<br/>
Ensuite, via l'application les usagers peuvent consulter les réservations effectuées sur les mois précédents ou actuel de l'année courante et ainsi avoir des statistiques sur leurs réservations.<br/>
Ces informations sont disponibles dans l'onglet : <b>Mon Profile</b>.<br/>
- Les admins peuvent superviser l'application en ayant le pouvoir de supprimer les réservations des autres utilisateurs et aussi ont accès à l'historique global des réservations.<br/>
Ces informations sont disponibles dans l'onglet : <b>Historique</b>.<br/>

## Specs

### Environnement :
- C# RazorPages ASP.NET / .NET 7
- JavaScript Natif
- Bootstrap
- EntityFramework

### Architecture :
- Hexagonale

### Autre :
- Middlewares
- Loggers
- Injections de dépendances

### Explication de l'architecture :
Cette application à été codée de manière à segmenter ses différentes parties.
Elle est composée de <b>6 bibliothèques de classes</b>.
#### Application :
Cette partie sert globalement au code fonctionnel de l'application, on y stock également les règles métier.<br/>
Ex: Je récupère les données de la BDD et j'effectue mon traitement, mon algorithme, je mets des exceptions pour protéger mon code.
- BusinessLogics : On y stock les règles métier.
- Exceptions : On y crée des customs exceptions pour mieux comprendre les anomalies de l'application pour certaines erreurs.
- Repositories : C'est le coeur de la bibliothèque. Ici on y met les fichiers pour coder l'application. Ces fichiers sont créés sur la base de la bibliothèque <b>Model</b>.
#### Global :
Cette partie sert à l'ensemble de l'application elle permet de superviser l'application pour que les différentes parties puissent accéder aux informations.<br/>
Ex: Je crée une variable d'environnement et je voudrais m'en servir dans la couche Application ou alors dans mon UI.
- Consts : On y stock les constantes de l'application.
- EnvVariables : On y stock les variables d'environnement.
- Utils : On y stock les méthodes utiles pour toute l'application.
#### Infrastructure :
Cette partie sert à la partie BDD, c'est d'ailleurs ici qu'on y met les requêtes à la BDD. On y met aussi les middlewares, logger, les encryptions. Cette partie à pour rôle de fournir des services de base à l'application, tels que l'accès à la base de données, la gestion des transactions, l'accès aux services externes, etc. Elle sert de "pont" entre les autres couches de l'application pour mettre en œuvre ces services.<br/>
Ex: Je fais une requête à ma BDD avec EntityFramework et je renvoie un objet à ma couche <b>Application</b>.
- Ciphers : On y stock les Algorithmes de chiffrement.
- Databases : On y configure nos <b>Models</b> pour les convertirs en table SQL.
- DataLayers : C'est le coeur de la bilbiothèque. Ici on y met les fichiers pour faire les requêtes à la BDD. Ces fichiers sont créers à partir du nom de nos tables donc nos <b>Models</b>.
- Loggers : On y stock nos customs loggers.
- Migrations : On y stock nos migrations de code en BDD (code first).
#### Infrastructure Web :
Cette partie sert d'interface web pour la partie infrastructure.<br/>
Ex: Je créer mes middlewares pour intercepter les requêtes et rediriger en fonctions des scénarios.
- Constraints : On y stock les contraintes. Utile si l'on souhaite mettre des conditions sur des models de données.
- Middlewares : On y stock les middlewares. Utile pour rediriger les requêtes.
#### Interfaces :
Cette partie sert à faire la jonction entre la partie DataLayers dans la couche Infrastructure et Repositories dans la couche Application.<br/>
Ex: Je créer mes interfaces pour respecter le contrat de mes classes parents. L'intérêt et de créer une rigueur dans le code.
- Infrastructures : On y stock les interfaces pour la couche Infrastructure.
- Repositories : On y stock les interfaces pour la couche Application.
#### Models :
Cette partie correspond en réalité à la couche <b>Domain</b> de l'architecture Hexagonale. On y stock nos models locaux ou nos models entity qui seront des models de tables SQL.<br/>
Ex: Je créer un model User qui sera converti ensuite en table SQL par mon Infrastructure et je pourrai l'alimenter dans mon DataLayer en retournant les data de la requête entity dans mon objet.
- (Racine) : On y stock nos models entity qui seront convertis en tables SQL et alimentés par mon DataLayer.
- DTOs : On y stock nos DTO qui servent d'objet pour les paramètres de mes fonctions.
- Locals : On y stock les objets locaux qui ne seront pas convertis en tables SQL dans la couche Infrastructure, mais seront rempli par mon DataLayer.

Pour résumer l'architecture hexagonale est composée normalement de 4 couches.
- Domain : Les models de la BDD.
- Application : Le code métier, le traitement des données, la partie algorithmique de l'application (le fonctionnel).
- Infrastructure : La supervision de l'application.
- Interfaces : Sert à la couche Application et Infrastructure de l'application.

Le projet fonctionne avec des injections de dépendances dans le fichier Program.cs.
L'objectif est de lier une interface à une classe pour pouvoir appeler l'interface dans le constructeur de nos classes.<br/>
Ex: Je lie IParkingSlotDataLayer à SqlServerParkingSlotDataLayer pour pouvoir l'injecter dans mon Repository dans la couche Application.

## Variables d'environnement :
Le projet est constitué de différentes variables d'environnement pour fonctionner correctement.
La personne en charge du déploiement de l'application devra déclarer ces variables sur le serveur client pour que l'application puisse fonctionner correctement.
- ConnectionString : Variable pour la chaine de connexion au serveur SQL.
- IsSso : Variable pour savoir si l'application va tourner sur le SSO de Keycloak ou si la connexion se fera sur la BDD interne.
- KeycloakPasswordUser : Variable pour définir le mot de passe du super utilisateur pour donner tous les droits à l'application de questionner l'API.
- WebsiteUri : Variable qui contient l'URL du site web / dns pour la redirection de Keycloak vers mon site après la déconnexion.

### Exemple concret :
- ConnectionString : Server=.;Database=GestionPlacesParking;Trusted_Connection=True;TrustServerCertificate=True
- IsSso : 1
- KeycloakPassword : pyLcVV$tAFRaNv3J
- WebsiteUri : https://localhost:7041

<b>Attention : </b>il faut créer un utilisateur dans le royaume Keycloak contenant ce nom d'utilisateur : <b>all_access_user</b>.<br/>
La connection string devra contenir : <b>TrustServerCertificate=True</b> afin d'éviter des erreurs lors du démarrage de l'application.

## Entity Framework -> Migrations :
Pour effectuer des migrations de code vers la BDD il faudra vous rendre dans le dossier <b>Infrastructure</b>, puis lancer la CMD ou PowerShell depuis cet endroit et vous pourrez exécuter les commandes suivantes :
- `dotnet ef migrations add "AddTableUser"`
- `dotnet ef database update`

La première commande permet d'ajouter une migration à la BDD.
La deuxième commande permet de mettre à jour les migrations effectuées par les autres utilisateurs.
