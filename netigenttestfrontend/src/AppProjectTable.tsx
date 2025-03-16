import { useEffect, useState } from "react";
import axios from "axios";
import { Table, Spinner, Card, Button } from "react-bootstrap";
import { AppProject } from "./Models/AppProject";
import { AppProjectIndividualViewModel } from "./Models/AppProjectIndividualViewModel";
import EditProjectModal from "./EditProjectModal";

const AppProjectTable = () => {
    const [projects, setProjects] = useState<AppProject[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [selectedProject, setSelectedProject] = useState<AppProjectIndividualViewModel | null>(null);
    const [showCreateModal, setShowCreateModal] = useState<boolean>(false);

    useEffect(() => {
        axios.get("/api/AppProject")
            .then(response => {
                const data = Array.isArray(response.data.$values) ? response.data.$values : [];
                setProjects(data);
            })
            .finally(() => setLoading(false));
    }, []);

    const handleEditClick = (id: number) => {
        axios.get(`/api/AppProject/${id}`)
            .then(response => {
                setSelectedProject(response.data);
            })
            .catch(error => {
                console.error("Error fetching project:", error);
            });
    };

    const handleCreateClick = () => {
        setShowCreateModal(true);
        setSelectedProject(null);
    };

    return (
        <div className="container mt-4">
            <h2 className="text-center mb-4">Applications</h2>
            <div className="d-flex justify-content-end mb-3">
                <Button variant="success" onClick={handleCreateClick}>
                    Create Application
                </Button>
            </div>
            {loading ? (
                <div className="d-flex justify-content-center">
                    <Spinner animation="border" />
                </div>
            ) : (
                <Card>
                    <Card.Body>
                        <div className="table-responsive">
                            <Table striped bordered hover size="sm">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Project Name</th>
                                        <th>Project Ref</th>
                                        <th>Project Location</th>
                                        <th>App Status</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {projects.map((project) => (
                                        <tr key={project.id}>
                                            <td>{project.id}</td>
                                            <td>{project.name || "N/A"}</td>
                                            <td>{project.reference || "N/A"}</td>
                                            <td>{project.location || "N/A"}</td>
                                            <td>{project.statusLevel || "N/A"}</td>
                                            <td>
                                                <Button variant="primary" onClick={() => handleEditClick(project.id)}>
                                                    Edit
                                                </Button>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </Table>
                        </div>
                    </Card.Body>
                </Card>
            )}

            {(selectedProject || showCreateModal) && (
                <EditProjectModal
                    show={selectedProject !== null || showCreateModal}
                    onHide={() => {
                        setSelectedProject(null);
                        setShowCreateModal(false);
                    }}
                    project={selectedProject}
                />
            )}
        </div>
    );
};

export default AppProjectTable;
