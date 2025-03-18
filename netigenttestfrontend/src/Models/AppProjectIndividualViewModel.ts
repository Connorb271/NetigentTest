export interface AppProjectIndividualViewModel {
    $id: number;
    id: number;
    appStatus?: string;
    projectRef?: string;
    projectName?: string;
    projectLocation?: string;
    openDt?: string;
    startDt?: string;
    completedDt?: string;
    projectValue: number;
    statusId: number;
    statusLevel?: string;
    notes?: string;
    modified?: string;
    isDeleted: boolean;
    inquiriesCount: number;
    inquiries?: InquiryViewModel[];
}

export interface InquiryViewModel {
    id: number;
    sendToPerson?: string;
    sendToRole?: string;
    sendToPersonId?: number;
    subject?: string;
    inquiryText?: string;
    response?: string;
    askedDt?: string;
    completedDt?: string;
}