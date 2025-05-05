# Projet API Minimale avec Entity Framework Core et ASP.NET Core

## Description
Ce projet est une API minimale développée dans le cadre du cours Techno .NET & C# à l'Université Cheikh Anta Diop, École Supérieure et Polytechnique (ESP), Département Génie Informatique. L'objectif est de découvrir comment utiliser une base de données avec une API minimale en utilisant Entity Framework Core et ASP.NET Core.

## Objectifs d'apprentissage
- Découvrir comment ajouter Entity Framework Core à une application API minimale.
- Persister des données dans un magasin de données en mémoire.
- Persister des données dans une base de données SQLite.
- Tester l'API.

## Prérequis
- .NET 6 ou supérieur
- Notions de base sur ce qu'est une API

## Introduction
Lorsque vous générez une application web qui traite des données, vous souhaitez probablement stocker ces données dans une base de données. Les API minimales générées sur ASP.NET Core peuvent être facilement intégrées à un grand nombre de bases de données à l'aide d'Entity Framework (EF) Core.

## Scénario
Vous êtes développeur dans une équipe. Vous avez généré une API qui gère les opérations de Création, Lecture, Mise à jour et Suppression (CRUD) sur une table de données. Vous envisagez de générer une application frontale qui utilise cette API. Vous souhaitez stocker les données dans une base de données pour pouvoir utiliser les données dans votre application frontale.

## Ce que vous allez apprendre
Vous allez apprendre à utiliser EF Core pour conserver vos données, d'abord dans une base de données en mémoire, puis dans SQLite. Vous allez également apprendre à utiliser EF Core pour interroger la base de données.

## Quel est l'objectif principal ?
Ajouter la prise en charge de base de données à une application API minimale.

## Qu'est-ce qu'Entity Framework Core ?
Entity Framework Core est une technologie d'accès aux données légère, extensible, open source et multiplateforme pour les applications .NET. Il peut servir de mappeur objet-relationnel qui permet aux développeurs .NET de travailler avec une base de données à l'aide d'objets .NET et élimine la nécessité d'une grande partie du code d'accès aux données qui doit généralement être écrit.

## Le modèle
Avec Entity Framework Core, l'accès aux données est effectué à l'aide d'un modèle. Un modèle est constitué de classes d'entité et d'un objet Context qui représente une session avec la base de données. L'objet Context permet l'interrogation et l'enregistrement des données.

## La classe d'entité
Dans ce scénario, vous implémentez une API de gestion des pizzas, vous allez donc utiliser une classe d'entité PizzaEhod. Les pizzas de votre magasin auront un nom et une description. Elles auront également besoin d'un ID pour permettre à l'API et à la base de données de les identifier.

```csharp
namespace Pizzéria.Models
{
    public class PizzaEhod
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Description { get; set; }
    }
}
```

## La classe Context
La classe Context est responsable de l'interrogation et de l'enregistrement des données dans vos classes d'entité, ainsi que de la création et de la gestion de la connexion de base de données.

## Effectuer des opérations CRUD avec EF Core
Après configuration d'EF Core, vous pouvez l'utiliser pour effectuer des opérations CRUD sur vos classes d'entité. Ensuite, vous pouvez développer sur des classes C#, en déléguant les opérations de base de données à la classe Context. Les fournisseurs de base de données le traduisent à leur tour en langage de requête spécifique à la base de données.

## Interroger des données
L'objet Context expose une classe collection pour chaque type d'entité. Dans l'exemple précédent, la classe Context expose une collection d'objets PizzaEhod en tant que Pizzas. Étant donné que nous disposons d'une instance de la classe Context, vous pouvez interroger la base de données pour toutes les pizzas :

```csharp
var pizzas = await db.Pizzas.ToListAsync();
```

## Insertion des données
Vous pouvez utiliser le même objet context pour insérer une nouvelle pizza :

```csharp
await db.pizzas.AddAsync(
    new PizzaEhod { ID = 1, Nom = "Pepperoni", Description = "La pizza classique au pepperoni" });
```

## Suppression de données
Les opérations de suppression sont simples. Elles n'ont besoin que d'un ID de l'élément à supprimer :

```csharp
var pizza = await db.pizzas.FindAsync(id);
if (pizza is null)
{
    //Handle error
}
db.pizzas.Remove(pizza);
```

## Mettre à jour des données
De même, vous pouvez mettre à jour une pizza existante :

```csharp
int id = 1;
var updatepizza = new PizzaEhod { Nom = "Ananas", Description = "Ummmm?" };
var pizza = await db.pizzas.FindAsync(id);
if (pizza is null)
{
    //Gérer une erreur
}
pizza.Item = updatepizza.Item;
pizza.IsComplete = updatepizza.IsComplete;
await db.SaveChangesAsync();
```

## Utiliser la base de données en mémoire d'EF Core
EF Core comprend un fournisseur de base de données en mémoire qui peut être utilisé pour tester l'application. Le fournisseur de base de données en mémoire est utile pour le test et le développement. Vous allez l'utiliser pour créer une base de données et effectuer des opérations CRUD sur celle-ci.

## Ajouter EF Core à l'API minimale
Ce module utilise l'interface CLI .NET et Visual Studio Code pour le développement local. Vous pouvez appliquer les concepts avec Visual Studio (Windows) ou Visual Studio pour Mac (macOS), ou poursuivre le développement avec Visual Studio Code (Windows, Linux et macOS).

## Configuration du projet
1. Dans un terminal, créez une API web en exécutant `dotnet new`.
2. Accédez au répertoire Pizzéria en entrant la commande suivante.
3. Installez le package Swashbuckle.
4. Ouvrez le projet dans Visual Studio / Visual Studio Code.
5. Créez un dossier Models et un fichier nommé PizzaEhod.cs dans ce dossier et donnez-lui le contenu suivant.

## Ajouter EF Core au projet
Installez le package EntityFrameworkCore.InMemory.
1. Ouvrez un terminal dans Visual Studio Code. Dans le nouveau terminal, entrez le code suivant pour ajouter le package en mémoire EF Core.
2. Ajoutez `using Microsoft.EntityFrameworkCore;` au début de vos fichiers Program.cs et PizzaEhod.cs.

## Retourner une liste d'éléments
Pour lire dans une liste d'éléments de la liste de pizzas, ajoutez le code suivant au-dessus de l'appel à `app.Run();` pour ajouter un itinéraire « /pizzas ».

## Exécution de l'application
1. Vérifiez que vous avez enregistré tous vos changements. Exécutez l'application. Cette action génère l'application et l'héberge sur un port compris entre 5000 et 5300. Un port HTTPS est sélectionné dans la plage comprise entre 7000 et 7300.
2. Dans votre navigateur, accédez à `https://localhost:{PORT}/swagger`. Sélectionnez le bouton GET /pizzas, suivi de Essayer et Exécuter. Vous verrez que la liste est vide sous Response body.
3. Dans le terminal, appuyez sur Ctrl + C pour arrêter l'exécution du programme.

## Créer des éléments
Ajoutons le code aux nouveaux éléments POST de la liste des pizzas. Dans Program.cs, ajoutez le code suivant sous le `app.MapGet` créé précédemment.

## Tester l'API
Vérifiez que vous avez enregistré toutes vos modifications et réexécutez l'application. Revenez à l'interface utilisateur Swagger, vous devriez maintenant voir POST/pizza. Pour ajouter de nouveaux éléments à la liste de pizzas :
1. Sélectionnez POST /pizza.
2. Sélectionnez Try it out.
3. Remplacez le corps de la requête par ce qui suit :
```json
{
 "name": "Pepperoni",
 "description": "Une pizza au pepperoni classique"
}
```
4. Sélectionnez Exécuter.

## Obtenir un seul élément
Pour obtenir (GET) un élément par id, ajoutez ce code sous l'itinéraire `app.MapPost` que vous avez créé précédemment.

## Mettre à jour un élément
Pour mettre à jour un élément existant, ajoutez ce code sous l'itinéraire GET /pizza/{id}.

## Supprimer un élément
Pour supprimer un élément, ajoutez ce code sous PUT /pizza/{id} créé précédemment.

## Utiliser le fournisseur de base de données SQLite avec EF Core
Dans cet exercice, vous mettrez à niveau votre application pour utiliser une base de données relationnelle pour stocker vos données. Vous utiliserez SQLite pour stocker vos données.

## Configurer la base de données SQLite
Dans le terminal, installez les packages suivants :
1. Fournisseur de base de données SQLite pour EF Core.
2. Outils EF Core.
3. Microsoft.EntityFrameworkCore.Design.

## Activer la création de bases de données
Pour activer la création de bases de données, vous devez définir la chaîne de connexion de base de données. Migrez alors votre modèle de données vers une base de données SQLite.

## Exécuter et tester l'application
Testez votre application avec `dotnet run` et l'interface utilisateur Swagger. Vérifiez que vos modifications sont conservées dans Pizzas.db. 
