import { openEditTaskModal } from './addTasksScript.js';
import { url, getTaskByStatusUrl, deleteTaskUrl } from './urls.js';


document.getElementById('filter-tasks-pendentes').addEventListener('click', () => getTasksByStatus(1));

document.getElementById('filter-tasks-emprogresso').addEventListener('click', () => getTasksByStatus(2));

document.getElementById('filter-tasks-concluidas').addEventListener('click', () => getTasksByStatus(3));


export function getTasks() {
    document.getElementById('tasks').innerHTML = '';
    fetch(url)
        .then(async response => await response.json())
        .then(tasks => tasks.forEach(addTaskToList))
}

export function getTasksByStatus(status) {
    document.getElementById('tasks').innerHTML = '';
    fetch(getTaskByStatusUrl(status))
        .then(async response => await response.json())
        .then(tasks => tasks.forEach(addTaskToList))
}

export function addTaskToList(task) {
    const item = createTaskElement(task);

    document.getElementById('tasks')
        .appendChild(item)
}

function createTaskElement(task) {
    const element = document.createElement('div');
    element.id = task.id;
    element.classList = ['taskItem']

    appendTasksElements(element, task);

    appendButtons(element, task);

    return element;
}

export function updateTaskInList(task) {
    const title = document.getElementById(getTaskTitleElementId(task));
    title.textContent = task.title;
    const description = document.getElementById(getTaskDescriptionElementId(task));
    description.textContent = task.description;
    const status = document.getElementById(getTaskStatusElementId(task));
    status.textContent = task.status;
    const creationDate = document.getElementById(getTaskCreationDateElementId(task));
    fillCreationDateContent(creationDate, task);
    const completionDate = document.getElementById(getTaskCompletionDateElementId(task));
    fillCompletionDateContent(completionDate, task);
}

function appendTasksElements(element, task) {
    const item = document.createElement('div');
    item.classList = ['tasks-elements']

    const title = createTitleElement(task)
    item.appendChild(title);

    const description = createDescriptionElement(task);
    item.appendChild(description);

    const status = createStatusElement(task);
    item.appendChild(status);

    const date = createDateElement(task);
    item.appendChild(date);

    element.appendChild(item);
}

function createTitleElement(task) {
    const element = document.createElement('span');
    element.id = getTaskTitleElementId(task);
    element.classList = ['title']
    element.textContent = task.title;
    return element;
}

function getTaskTitleElementId(task) {
    return `task-title-${task.id}`;
}

function createDescriptionElement(task) {
    const element = document.createElement('div');
    element.id = getTaskDescriptionElementId(task);
    element.classList = ['description']
    element.textContent = task.description;
    return element;
}

function getTaskDescriptionElementId(task) {
    return `task-description-${task.id}`;
}

function createStatusElement(task) {
    const element = document.createElement('div');
    element.id = getTaskStatusElementId(task);
    element.classList = ['status'];
    element.textContent = task.status;
    return element;
}

function getTaskStatusElementId(task) {
    return `task-status-${task.id}`;
}

function createDateElement(task) {
    const element = document.createElement('div');
    element.classList = ['task-dates'];

    const creationDate = createCreationDateElement(task);
    element.appendChild(creationDate);

    const completionDate = createCompletionDateElement(task);
    element.appendChild(completionDate);

    return element;
}

function createCreationDateElement(task) {
    const creationDate = document.createElement('div');
    creationDate.id = getTaskCreationDateElementId(task);
    creationDate.classList = ['creation-date'];
    fillCreationDateContent(creationDate, task);
    return creationDate;
}

function fillCreationDateContent(element, task) {
    if (task.createdAt)
        element.textContent = `Criado em ${formatDate(task.createdAt)}`;
    else
        element.textContent = '';
}

function getTaskCreationDateElementId(task) {
    return `task-creation-date-${task.id}`;
}

function createCompletionDateElement(task) {
    const completionDate = document.createElement('div');
    completionDate.id = getTaskCompletionDateElementId(task);
    completionDate.classList = ['completion-date'];
    fillCompletionDateContent(completionDate, task);
    return completionDate;
}

function fillCompletionDateContent(element, task) {
    if (task.completedAt)
        element.textContent = `Concluido em ${formatDate(task.completedAt)}`;
    else
        element.textContent = '';
}

function getTaskCompletionDateElementId(task) {
    return `task-completion-date-${task.id}`;
}

function formatDate(dateString) {
    const date = new Date(dateString);

    const formattedDate = date.toLocaleString('pt-BR', {
        year: 'numeric', // "2025"
        month: 'long',   // "April"
        day: 'numeric',  // "10"
    });

    return formattedDate;
}

function appendButtons(element, task) {
    const item = document.createElement('div');
    item.classList = ['buttons-elements']

    const deleteButton = createDeleteButton(task);
    item.appendChild(deleteButton);

    const editButton = createEditButton(task);
    item.appendChild(editButton);

    element.appendChild(item);
}

function createDeleteButton(task) {
    const button = document.createElement('button');
    button.classList = ['button'];
    button.onclick = ev => deleteTask(task);
    button.textContent = 'Excluir';
    return button;
}

function deleteTask(task) {
    fetch(deleteTaskUrl(task.id), { method: 'DELETE' })
        .then(async response => {
            if (response.ok) {
                var deletedElement = document.getElementById(task.id);
                document.getElementById('tasks').removeChild(deletedElement);
            }
        })
}

function createEditButton(task) {
    const button = document.createElement('button');
    button.classList = ['button'];
    button.onclick = ev => openEditTaskModal(task);
    button.textContent = 'Editar';
    return button;
}
