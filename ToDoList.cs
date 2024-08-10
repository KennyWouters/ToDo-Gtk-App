using System;
using Gtk;

class ToDoListApp
{
    static void Main()
    {
        Application.Init();

        // Créer la fenêtre principale
        Window mainWindow = new Window("To-Do List");
        mainWindow.SetDefaultSize(400, 300);
        mainWindow.DeleteEvent += delegate { Application.Quit(); };

        // Créer une boîte verticale pour organiser les widgets
        Box vbox = new Box(Orientation.Vertical, 5);

        // Créer un champ de texte pour entrer les tâches
        Entry taskEntry = new Entry();
        vbox.PackStart(taskEntry, false, false, 5);

        // Créer un bouton pour ajouter les tâches
        Button addButton = new Button("Add Task");
        vbox.PackStart(addButton, false, false, 5);

        // Créer une liste pour afficher les tâches
        ListBox taskList = new ListBox();
        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.Add(taskList);
        vbox.PackStart(scrolledWindow, true, true, 5);

        // Créer un bouton pour supprimer les tâches sélectionnées
        Button removeButton = new Button("Remove Selected Task");
        vbox.PackStart(removeButton, false, false, 5);

        // Ajouter la boîte à la fenêtre principale
        mainWindow.Add(vbox);

        // Gestion de l'événement pour ajouter une tâche
        addButton.Clicked += (sender, e) =>
        {
            if (!string.IsNullOrWhiteSpace(taskEntry.Text))
            {
                ListBoxRow row = new ListBoxRow();
                Label label = new Label(taskEntry.Text);
                row.Add(label);
                row.ShowAll();
                taskList.Add(row);
                taskEntry.Text = string.Empty;
            }
            else
            {
                ShowMessage(mainWindow, "Please enter a valid task.");
            }
        };

        // Gestion de l'événement pour supprimer une tâche sélectionnée
        removeButton.Clicked += (sender, e) =>
        {
            var selectedRow = taskList.SelectedRow;
            if (selectedRow != null)
            {
                taskList.Remove(selectedRow);
            }
            else
            {
                ShowMessage(mainWindow, "Please select a task to remove.");
            }
        };

        // Afficher la fenêtre et démarrer l'application
        mainWindow.ShowAll();
        Application.Run();
    }

    static void ShowMessage(Window parentWindow, string message)
    {
        MessageDialog md = new MessageDialog(
            parentWindow,
            DialogFlags.Modal,
            MessageType.Info,
            ButtonsType.Ok,
            message
        );
        md.Run();
        md.Destroy();
    }
}