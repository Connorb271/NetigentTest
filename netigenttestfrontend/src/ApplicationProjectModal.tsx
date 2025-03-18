import React, { useEffect, useState } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import axios from 'axios';

interface ApplicationProjectModalProps {
    isEditing: boolean;
    appId: number;
    show: boolean;
    handleClose: () => void;
}

const ApplicationProjectModal: React.FC<ApplicationProjectModalProps> = ({ isEditing, appId, show, handleClose }) => {
    const [projectData, setProjectData] = useState<any>(null);
    const [statusOptions, setStatusOptions] = useState<any[]>([]); // To store status options for select
    const [formData, setFormData] = useState({
        projectName: '',
        projectRef: '',
        projectLocation: '',
        openDt: '',
        startDt: '',
        completedDt: '',
        projectValue: 0,
        statusId: 0,
        notes: '',
    });

    useEffect(() => {
        if (isEditing && show) {
            axios.get(`/api/appproject/${appId}`)
                .then(response => {
                    setProjectData(response.data);
                    setFormData({
                        projectName: response.data.projectName || '',
                        projectRef: response.data.projectRef || '',
                        projectLocation: response.data.projectLocation || '',
                        openDt: response.data.openDt ? new Date(response.data.openDt).toISOString().split('T')[0] : '',
                        startDt: response.data.startDt ? new Date(response.data.startDt).toISOString().split('T')[0] : '',
                        completedDt: response.data.completedDt ? new Date(response.data.completedDt).toISOString().split('T')[0] : '',
                        projectValue: response.data.projectValue || 0,
                        statusId: response.data.statusId || 0,
                        notes: response.data.notes || '',
                    });
                })
                .catch(error => console.error('Error fetching project data:', error));
        }

        axios.get('/api/statuslevel')
            .then(response => setStatusOptions(response.data))
            .catch(error => console.error('Error fetching status options:', error));
    }, [isEditing, show, appId]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData(prevData => ({
            ...prevData,
            [name]: value
        }));
    };

    const handleSubmit = () => {
        if (isEditing) {
            axios.put(`/api/appproject/${appId}`, formData)
                .then(response => {
                    console.log('Project updated successfully', response.data);
                    handleClose();
                })
                .catch(error => console.error('Error updating project:', error));
        } else {
            axios.post('/api/appproject', formData)
                .then(response => {
                    console.log('Project created successfully', response.data);
                    handleClose();
                })
                .catch(error => console.error('Error creating project:', error));
        }
    };

    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>{isEditing ? 'Edit Application Project' : 'Create Application Project'}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group controlId="projectName">
                        <Form.Label>Project Name</Form.Label>
                        <Form.Control
                            type="text"
                            name="projectName"
                            value={formData.projectName}
                            onChange={handleChange}
                            placeholder="Enter project name"
                        />
                    </Form.Group>
                    <Form.Group controlId="projectRef">
                        <Form.Label>Project Reference</Form.Label>
                        <Form.Control
                            type="text"
                            name="projectRef"
                            value={formData.projectRef}
                            onChange={handleChange}
                            placeholder="Enter project reference"
                        />
                    </Form.Group>
                    <Form.Group controlId="projectLocation">
                        <Form.Label>Project Location</Form.Label>
                        <Form.Control
                            type="text"
                            name="projectLocation"
                            value={formData.projectLocation}
                            onChange={handleChange}
                            placeholder="Enter project location"
                        />
                    </Form.Group>
                    <Form.Group controlId="openDt">
                        <Form.Label>Open Date</Form.Label>
                        <Form.Control
                            type="date"
                            name="openDt"
                            value={formData.openDt}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId="startDt">
                        <Form.Label>Start Date</Form.Label>
                        <Form.Control
                            type="date"
                            name="startDt"
                            value={formData.startDt}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId="completedDt">
                        <Form.Label>Completed Date</Form.Label>
                        <Form.Control
                            type="date"
                            name="completedDt"
                            value={formData.completedDt}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId="projectValue">
                        <Form.Label>Project Value</Form.Label>
                        <Form.Control
                            type="number"
                            name="projectValue"
                            value={formData.projectValue}
                            onChange={handleChange}
                            placeholder="Enter project value"
                        />
                    </Form.Group>
                    <Form.Group controlId="statusId">
                        <Form.Label>Status</Form.Label>
                        <Form.Control
                            as="select"
                            name="statusId"
                            value={formData.statusId}
                            onChange={handleChange}
                        >
                            <option value={0}>Select Status</option>
                            {statusOptions.map(status => (
                                <option key={status.id} value={status.id}>
                                    {status.statusName}
                                </option>
                            ))}
                        </Form.Control>
                    </Form.Group>
                    <Form.Group controlId="notes">
                        <Form.Label>Notes</Form.Label>
                        <Form.Control
                            as="textarea"
                            name="notes"
                            value={formData.notes}
                            onChange={handleChange}
                            rows={3}
                            placeholder="Enter project notes"
                        />
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>Close</Button>
                <Button variant="primary" onClick={handleSubmit}>
                    {isEditing ? 'Save Changes' : 'Create Project'}
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

export default ApplicationProjectModal;
