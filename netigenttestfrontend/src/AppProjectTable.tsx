import React, { useEffect, useState } from "react";
import axios from "axios";
import { Button, Table, Container, Modal } from "react-bootstrap";
import ApplicationProjectModal from './ApplicationProjectModal'; // Import the modal

interface AppProjectSearchViewModel {
    id: number;
    name: string | null;
    reference: string | null;
    location: string | null;
    statusLevel: string | null;
}

const ProjectPage: React.FC = () => {
    const [projects, setProjects] = useState<AppProjectSearchViewModel[]>([]);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [projectToDelete, setProjectToDelete] = useState<AppProjectSearchViewModel | null>(null);
    const [projectToEdit, setProjectToEdit] = useState(0);

    useEffect(() => {
        getProjects();
    }, []);

    const getProjects = () => {
        axios.get('/api/appproject')
            .then(response => setProjects(response.data))
            .catch(error => console.error('Error fetching projects:', error));
    }

    const handleCreate = () => {
        setShowCreateModal(true);
        console.log("Create new project");
    };

    const handleEdit = (id: number) => {
        console.log("Edit project with ID:", id);
        setProjectToEdit(id);
        setShowEditModal(true);
    };

    const handleDelete = (id: number) => {
        const project = projects.find(p => p.id === id);
        if (project) {
            setProjectToDelete(project);
            setShowDeleteModal(true);
        }
    };

    const confirmDelete = () => {
        if (projectToDelete) {
            axios.delete(`/api/appproject/${projectToDelete.id}`)
                .then(() => {
                    setProjects(projects.filter(project => project.id !== projectToDelete.id));
                    setShowDeleteModal(false);
                    setProjectToDelete(null);
                })
                .catch(error => {
                    console.error('Error deleting project:', error);
                    setShowDeleteModal(false);
                });
        }
    };

    const cancelDelete = () => {
        setShowDeleteModal(false);
        setProjectToDelete(null);
    };

    const closeEditModal = () => {
        setShowCreateModal(false);
        setShowEditModal(false);
        getProjects();
    };

    return (
        <Container className="mt-4">
            {showEditModal && (
                <ApplicationProjectModal
                    isEditing={true}
                    appId={projectToEdit}
                    show={showEditModal}
                    handleClose={closeEditModal}
                />
            )}
            {showCreateModal && (
                <ApplicationProjectModal
                    isEditing={false}
                    appId={0}
                    show={showCreateModal}
                    handleClose={closeEditModal}
                />
            )}

            <h1>Project List</h1>
            <Button variant="primary" onClick={handleCreate} className="mb-3">
                Create Application
            </Button>

            <div className="table-responsive">
                <Table striped bordered hover responsive>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Reference</th>
                            <th>Location</th>
                            <th>Status Level</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {projects.map(project => (
                            <tr key={project.id}>
                                <td>{project.id}</td>
                                <td>{project.name}</td>
                                <td>{project.reference}</td>
                                <td>{project.location}</td>
                                <td>{project.statusLevel}</td>
                                <td>
                                    <Button variant="warning" onClick={() => handleEdit(project.id)}>Edit</Button>
                                    <Button variant="danger" onClick={() => handleDelete(project.id)}>Delete</Button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            </div>

            <Modal show={showDeleteModal} onHide={cancelDelete}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm Deletion</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Are you sure you want to delete this project?
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={cancelDelete}>Cancel</Button>
                    <Button variant="danger" onClick={confirmDelete}>Confirm</Button>
                </Modal.Footer>
            </Modal>
        </Container>
    );
};

export default ProjectPage;
