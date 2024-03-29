﻿using AutoMapper;
using ExamApi.DotNet.Domain.Data.Dtos;
using ExamApi.DotNet.Domain.Entity;
using ExameApi.DotNet.Application.Service.Interface;
using ExameApi.DotNet.Domain.Entity.Enum;
using ExameApi.DotNet.Repository.Interface;
using Newtonsoft.Json;

namespace ExameApi.DotNet.Application.Service;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    private List<Exam> exams;

    public ExamService(IExamRepository examRepository, IPatientRepository patientRepository, IMapper mapper)
    {
        _examRepository = examRepository;
        _patientRepository = patientRepository;
        _mapper = mapper;

        string json = File.ReadAllText("exams.json");
        exams = JsonConvert.DeserializeObject<List<Exam>>(json);
    }

    public async Task<Exam> Save(ExamDto examDto)
    {
        var examConverter = _mapper.Map<Exam>(examDto);

        examConverter.Patient = await _patientRepository.FindById(examDto.IdPatient);

        var weatherSaved = await _examRepository.Save(examConverter);

        return weatherSaved;
    }
    
    public async Task<List<ExamDto>> GetExamsByAgeAndGender(int? age, Gender? gender)
    {
        var exams = await _examRepository.GetExamsByAgeAndGender(age, gender);

        List<string> requiredExams = new List<string>();

        if (age >= 0 && age <= 100)
        {
            requiredExams.AddRange(new List<string> { "Hemograma completo", "Exame de urina", "Glicemia", "Eletrocardiograma" });
        }

        if (gender == Gender.F && age >= 25 && age <= 64)
        {
            requiredExams.Add("Papanicolau");
        }

        if (gender == Gender.F && age >= 40 && age <= 100)
        {
            requiredExams.Add("Mamografia");
        }

        if (gender == Gender.M && age >= 40 && age <= 100)
        {
            requiredExams.AddRange(new List<string> { "PSA e toque retal" });
        }

        if (age >= 0 && age <= 12)
        {
            requiredExams.AddRange(new List<string> { "Parasitológico", "Glicemia e insulina", "Sorologia" });
        }

        if (age > 12 && age <= 18)
        {
            requiredExams.AddRange(new List<string> { "Lipidograma", "PCR" });
        }

        if (age >= 35 && age <= 100)
        {
            requiredExams.Add("TSH e T4");
        }

        if (age >= 40 && age <= 100)
        {
            requiredExams.Add("Teste ergométrico");
        }

        if (age >= 50 && age <= 100)
        {
            requiredExams.Add("Densitometria óssea");
        }

        var filteredExams = exams.Where(exam => requiredExams.Contains(exam.Name)).ToList();

        return _mapper.Map<List<ExamDto>>(filteredExams);
    }

    public async Task<IEnumerable<Exam>> FindAll()
    {
        var examsData = await _examRepository.FindAll();

        return examsData;
    }

    public async Task<Exam> FindById(Guid id, bool tracking = true)
    {
        Console.WriteLine($"Searching for exam with ID: {id}");
        var examData = await _examRepository.FindById(id, tracking);
        Console.WriteLine($"Found exam: {examData}");
        return examData;
    }

}
