import { addTaskToList, updateTaskInList, getTasks, getTasksByStatus } from './getTasksScript.js'
import { url } from './urls.js'


document.getElementById('add-task-button').addEventListener('click', openAddTaskModal);

document.getElementById('save-task-button').addEventListener('click', addTask);

document.getElementById('add-task-form').addEventListener('submit', ev => addTask(ev));

document.getElementById('close-modal').addEventListener('click', hideAddTaskModal);


let editTask;

export function openEditTaskModal(task) {
    openAddTaskModal();
    editTask = task;
    document.getElementById('input-title').value = task.title;
    document.getElementById('input-description').value = task.description;
    document.getElementById('input-status').value = task.status;
}

function openAddTaskModal() {
    editTask = null;
    var element = document.getElementById('add-task-modal');
    element.style.visibility = "visible";
}

function hideAddTaskModal() {
    cleanTitleInput();
    cleanDescriptionInput();
    hideErroMessage();
    var element = document.getElementById('add-task-modal');
    element.style.visibility = "hidden";
}

function cleanTitleInput() {
    var titleElement = document.getElementById('input-title');
    titleElement.value = '';
}

function cleanDescriptionInput() {
    var descriptionElement = document.getElementById('input-description');
    descriptionElement.value = '';
}

function addTask(event) {
    event.preventDefault();
    const body = {
        title: document.getElementById('input-title').value,
        description: document.getElementById('input-description').value,
        status: document.getElementById('input-status').value
    }

    let method = 'POST';

    if (editTask) {
        method = 'PUT';
        body.id = editTask.id;
    }

    fetch(url, {
        method: method,
        body: JSON.stringify(body),
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
    }).then(async response => await updateTaskList(response));
}

async function updateTaskList(response) {
    var content = await response.json();

    if (!response.ok) {
        showErrorMessage(content);
        return;
    }

    if (editTask) {
        updateTaskInList(content);
    } else {
        addTaskToList(content);
    }
    hideAddTaskModal();
}

function showErrorMessage(text) {
    document.getElementById('error-message').textContent = text.join('\n');
}

function hideErroMessage() {
    document.getElementById('error-message').textContent = '';
}

document.addEventListener('DOMContentLoaded', () => {
    hideAddTaskModal();
    getTasks();
})