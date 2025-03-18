import React, { useEffect, useState } from 'react';
import { Modal, Button, Form, Row, Col, Card } from 'react-bootstrap';
import axios from 'axios';

interface ApplicationProjectModalProps {
    isEditing: boolean;
    appId: number;
    show: boolean;
    handleClose: () => void;
}

const ApplicationProjectModal: React.FC<ApplicationProjectModalProps> = ({ isEditing, appId, show, handleClose }) => {
    const [projectData, setProjectData] = useState<any>(null);
    const [statusOptions, setStatusOptions] = useState<any[]>([]);
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
    const [inquiryModalShow, setInquiryModalShow] = useState(false);
    const [subject, setSubject] = useState('');
    const [inquiryText, setInquiryText] = useState('');
    const [deleteModalShow, setDeleteModalShow] = useState(false);
    const [inquiryToDelete, setInquiryToDelete] = useState<number | null>(null);

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

    const handleInquiryModalClose = () => setInquiryModalShow(false);

    const handleInquiryModalOpen = () => setInquiryModalShow(true);

    const handleInquirySubmit = () => {
        const inquiryData = {
            appProjectId: appId,
            subject,
            inquiryText
        };

        axios.post('/api/inquiry', inquiryData)
            .then(response => {
                console.log('Inquiry created successfully', response.data);
                setSubject('');
                setInquiryText('');
                setProjectData(prevData => ({
                    ...prevData,
                    inquiries: [...prevData.inquiries, response.data]
                }));
                handleInquiryModalClose();
            })
            .catch(error => console.error('Error creating inquiry:', error));
    };

    const handleDeleteModalClose = () => setDeleteModalShow(false);

    const handleDeleteInquiry = (inquiryId: number) => {
        setInquiryToDelete(inquiryId);
        setDeleteModalShow(true);
    };

    const confirmDeleteInquiry = () => {
        if (inquiryToDelete) {
            axios.delete(`/api/inquiry/${inquiryToDelete}`)
                .then(response => {
                    console.log('Inquiry deleted successfully', response.data);
                    setProjectData(prevData => ({
                        ...prevData,
                        inquiries: prevData.inquiries.filter((inq: any) => inq.id !== inquiryToDelete)
                    }));
                    handleDeleteModalClose();
                })
                .catch(error => console.error('Error deleting inquiry:', error));
        }
    };

    return (
        <>
            <Modal show={show} onHide={handleClose} size="lg">
                <Modal.Header closeButton>
                    <Modal.Title>{isEditing ? 'Edit Application Project' : 'Create Application Project'}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Row>
                        <Col md={6}>
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
                        </Col>
                        <Col md={6}>
                            <Button
                                variant="success"
                                onClick={handleInquiryModalOpen}
                                disabled={!isEditing}
                            >
                                Add Inquiry
                            </Button>
                            {projectData?.inquiries?.length > 0 && (
                                <div className="mt-4">
                                    {projectData.inquiries.map((inquiry: any) => (
                                        <Card key={inquiry.id} className="mb-3">
                                            <Card.Body>
                                                <Card.Title>{inquiry.subject}</Card.Title>
                                                <Card.Text>
                                                    <strong>Send To:</strong> {inquiry.sendToPerson} ({inquiry.sendToRole})<br />
                                                    <strong>Asked Date:</strong> {inquiry.askedDt ? new Date(inquiry.askedDt).toLocaleDateString() : 'N/A'}<br />
                                                    <strong>Response:</strong> {inquiry.response || 'No response yet'}<br />
                                                    <strong>Inquiry Text:</strong> {inquiry.inquiryText}
                                                </Card.Text>
                                                <Button
                                                    variant="danger"
                                                    onClick={() => handleDeleteInquiry(inquiry.id)}
                                                >
                                                    Delete
                                                </Button>
                                            </Card.Body>
                                        </Card>
                                    ))}
                                </div>
                            )}
                        </Col>
                    </Row>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>Close</Button>
                    <Button variant="primary" onClick={handleSubmit}>{isEditing ? 'Update' : 'Create'} Project</Button>
                </Modal.Footer>
            </Modal>

            <Modal show={inquiryModalShow} onHide={handleInquiryModalClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Create Inquiry</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group controlId="subject">
                            <Form.Label>Subject</Form.Label>
                            <Form.Control
                                type="text"
                                value={subject}
                                onChange={(e) => setSubject(e.target.value)}
                                placeholder="Enter subject"
                            />
                        </Form.Group>
                        <Form.Group controlId="inquiryText">
                            <Form.Label>Inquiry Text</Form.Label>
                            <Form.Control
                                as="textarea"
                                value={inquiryText}
                                onChange={(e) => setInquiryText(e.target.value)}
                                rows={3}
                                placeholder="Enter inquiry text"
                            />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleInquiryModalClose}>Close</Button>
                    <Button variant="primary" onClick={handleInquirySubmit}>Create Inquiry</Button>
                </Modal.Footer>
            </Modal>

            <Modal show={deleteModalShow} onHide={handleDeleteModalClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Delete Inquiry</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    Are you sure you want to delete this inquiry?
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleDeleteModalClose}>Cancel</Button>
                    <Button variant="danger" onClick={confirmDeleteInquiry}>Delete</Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};

export default ApplicationProjectModal;
