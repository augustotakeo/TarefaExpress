const baseUrl = "http://localhost:5212";

export const url = `${baseUrl}/tasks`;

export const getTaskByStatusUrl = status => `${baseUrl}/tasks/status/${status}`;

export const deleteTaskUrl = id => `${baseUrl}/tasks/${id}`